document.addEventListener("DOMContentLoaded", function () {

    const revealElements = document.querySelectorAll(
        `
        .about-card,
        .featured-project-card,
        .skill-item,
        .language-card,
        .achievement-card,
        .testimonial-card,

        .projects-grid .project-card,

        .project-detail-section,
        .project-detail-card,
        .detail-feature-card,
        .project-detail-gallery,
        .uav-detail-card,
        .training-detail-card,
        .project-detail-bottom,

        .certificate-modern-card,
        .reference-modern-card,

        .contact-form-card,
        .contact-info-card
        `
    );

    revealElements.forEach((element, index) => {

        element.classList.add("reveal");

        if (index % 3 === 1) {
            element.classList.add("reveal-delay-1");
        }

        if (index % 3 === 2) {
            element.classList.add("reveal-delay-2");
        }

    });


    const observer = new IntersectionObserver(
        entries => {

            entries.forEach(entry => {

                if (entry.isIntersecting) {

                    entry.target.classList.add(
                        "reveal-visible"
                    );

                    observer.unobserve(entry.target);
                }

            });

        },
        {
            threshold: 0.12
        }
    );


    revealElements.forEach(element => {
        observer.observe(element);
    });

});