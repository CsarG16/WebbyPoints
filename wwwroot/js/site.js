document.addEventListener('DOMContentLoaded', function () {

    const navbar = document.querySelector('.wp-navbar');
    const hero = document.querySelector('#heroSection');

    // NAVBAR SCROLL
    window.addEventListener('scroll', function () {

        if (!hero) return;

        const heroBottom = hero.getBoundingClientRect().bottom;

        if (heroBottom <= 80) {
            navbar.classList.add('scrolled');
        } else {
            navbar.classList.remove('scrolled');
        }

    });

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