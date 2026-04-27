document.addEventListener('DOMContentLoaded', function () {

    // SOLO enlaces internos (#)
    document.querySelectorAll('a[href^="#"]').forEach(function (anchor) {
        anchor.addEventListener('click', function (e) {

            const targetId = this.getAttribute('href');

            // 🔥 IMPORTANTE: evitar bloquear links reales
            if (targetId === "#" || targetId.length <= 1) return;

            const target = document.querySelector(targetId);

            if (target) {
                e.preventDefault();
                target.scrollIntoView({
                    behavior: 'smooth'
                });
            }
        });
    });

});