using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.IO;

namespace WebbyPoints.ML.Recommender
{
    public class RecommenderModelTraining
    {
        private static string DATA_FILEPATH = Path.Combine(Directory.GetCurrentDirectory(), "ML", "Recommender", "trainingdata", "checkin_training_data.csv");
        private static string MODEL_FILEPATH = Path.Combine(Directory.GetCurrentDirectory(), "ML", "Recommender", "RecommenderModel.mlnet");

        /// <summary>
        /// Ejecuta el proceso de entrenamiento optimizado con FastTree Regression y evalúa su precisión.
        /// </summary>
        public static void TrainModel()
        {
            // 1. Inicializar MLContext
            var mlContext = new MLContext(seed: 1);

            Console.WriteLine("[IA] Cargando datos de entrenamiento...");
            if (!File.Exists(DATA_FILEPATH))
            {
                throw new FileNotFoundException($"No se pudo encontrar el archivo de entrenamiento en {DATA_FILEPATH}");
            }

            // 2. Cargar los datos desde el archivo CSV
            IDataView dataView = mlContext.Data.LoadFromTextFile<RecommenderModel.ModelInput>(
                path: DATA_FILEPATH,
                hasHeader: true,
                separatorChar: ','
            );

            // 3. Dividir los datos en Entrenamiento (80%) y Prueba (20%) para evaluar precisión
            var trainTestSplit = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2, seed: 1);
            var trainSet = trainTestSplit.TrainSet;
            var testSet = trainTestSplit.TestSet;

            // 4. Crear el pipeline de entrenamiento optimizado
            // - Codificamos en One Hot las columnas categóricas (Carrera, Preferencia, Categoría)
            // - Concatenamos todo en 'Features'
            // - Usamos FastTree Regression (Algoritmo avanzado de Gradient Boosting sobre árboles de decisión)
            var pipeline = mlContext.Transforms.Categorical.OneHotEncoding(@"Carrera", @"Carrera")
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(@"Preferencia", @"Preferencia"))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(@"PuntoCategoria", @"PuntoCategoria"))
                .Append(mlContext.Transforms.Concatenate(@"Features", @"Carrera", @"Preferencia", @"PuntoCategoria", @"PuntoInteresId"))
                .Append(mlContext.Regression.Trainers.FastTree(
                    labelColumnName: @"Visito", 
                    featureColumnName: @"Features",
                    numberOfLeaves: 20,
                    numberOfTrees: 100,
                    minimumExampleCountPerLeaf: 2
                ));

            Console.WriteLine("[IA] Entrenando el recomendador usando FastTree Regression (Gradient Boosting)...");
            var model = pipeline.Fit(trainSet);

            // 5. Evaluar la precisión matemática del modelo con los datos de prueba
            Console.WriteLine("[IA] Evaluando precisión con los datos de prueba (20%)...");
            var predictions = model.Transform(testSet);
            var metrics = mlContext.Regression.Evaluate(predictions, labelColumnName: "Visito", scoreColumnName: "Score");

            // Imprimir métricas de precisión hiper-detalladas en consola
            Console.WriteLine("\n=========================================================================");
            Console.WriteLine("📊 INFORME DE PRECISIÓN DE LA INTELIGENCIA ARTIFICIAL (ML.NET)");
            Console.WriteLine("=========================================================================");
            Console.WriteLine($"* Coeficiente de Determinación (R²): {metrics.RSquared:P2} (Precisión en varianza)");
            Console.WriteLine($"* Error Absoluto Medio (MAE):       {metrics.MeanAbsoluteError:F4} (Error promedio)");
            Console.WriteLine($"* Error Cuadrático Medio (RMSE):    {metrics.RootMeanSquaredError:F4} (Penalizador de desvíos)");
            Console.WriteLine("=========================================================================");

            // 6. Guardar el modelo en la carpeta del proyecto
            Console.WriteLine($"[IA] Guardando el modelo entrenado en: {MODEL_FILEPATH}");
            mlContext.Model.Save(model, dataView.Schema, MODEL_FILEPATH);

            // También guardamos una copia directa en el directorio binario de ejecución del servidor
            string binModelPath = Path.Combine(AppContext.BaseDirectory, "RecommenderModel.mlnet");
            Console.WriteLine($"[IA] Guardando copia de ejecución en: {binModelPath}");
            mlContext.Model.Save(model, dataView.Schema, binModelPath);

            Console.WriteLine("[IA] ¡Entrenamiento completado y modelo guardado con éxito!\n");
        }
    }
}
