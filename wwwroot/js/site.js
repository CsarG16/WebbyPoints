document.addEventListener('DOMContentLoaded', function () {

    const navbar = document.querySelector('.wp-navbar');
    const hero = document.querySelector('#heroSection');

    window.addEventListener('scroll', function () {

        if (!hero) return;

        const heroBottom = hero.getBoundingClientRect().bottom;

        // Cuando el hero ya salió de la pantalla
        if (heroBottom <= 80) {
            navbar.classList.add('scrolled');
        } else {
            navbar.classList.remove('scrolled');
        }

    });

});