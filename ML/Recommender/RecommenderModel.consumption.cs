using Microsoft.ML;
using Microsoft.ML.Data;
using System.IO;

namespace WebbyPoints.ML.Recommender
{
    public class RecommenderModel
    {
        public class ModelInput
        {
            [ColumnName(@"Carrera"), LoadColumn(0)]
            public string Carrera { get; set; } = string.Empty;

            [ColumnName(@"Preferencia"), LoadColumn(1)]
            public string Preferencia { get; set; } = string.Empty;

            [ColumnName(@"PuntoInteresId"), LoadColumn(2)]
            public float PuntoInteresId { get; set; }

            [ColumnName(@"PuntoCategoria"), LoadColumn(3)]
            public string PuntoCategoria { get; set; } = string.Empty;

            [ColumnName(@"Visito"), LoadColumn(4)]
            public float Visito { get; set; }
        }

        public class ModelOutput
        {
            [ColumnName(@"Score")]
            public float Score { get; set; }
        }

        private static string MLNetModelPath = Path.Combine(AppContext.BaseDirectory, "RecommenderModel.mlnet");

        public static readonly Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(() => CreatePredictEngine(), true);

        private static PredictionEngine<ModelInput, ModelOutput> CreatePredictEngine()
        {
            var mlContext = new MLContext();
            
            // Si el modelo no está en el directorio de ejecución de binarios, buscamos en la estructura del proyecto
            if (!File.Exists(MLNetModelPath))
            {
                var fallbackPath = Path.Combine(Directory.GetCurrentDirectory(), "ML", "Recommender", "RecommenderModel.mlnet");
                if (File.Exists(fallbackPath))
                {
                    MLNetModelPath = fallbackPath;
                }
            }

            if (!File.Exists(MLNetModelPath))
            {
                throw new FileNotFoundException($"No se pudo encontrar el archivo del modelo entrenado en {MLNetModelPath}. Por favor, entrena el modelo primero.");
            }

            ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var _);
            return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
        }

        /// <summary>
        /// Realiza una predicción sobre qué tanto le gustará un lugar a un estudiante
        /// en base a su carrera y preferencias personales.
        /// </summary>
        /// <param name="input">Datos de entrada (carrera del usuario, preferencia, id y categoría del lugar)</param>
        /// <returns>Puntuación de afinidad estimada (0.0 a 1.0)</returns>
        public static ModelOutput Predict(ModelInput input)
        {
            return PredictEngine.Value.Predict(input);
        }
    }
}
