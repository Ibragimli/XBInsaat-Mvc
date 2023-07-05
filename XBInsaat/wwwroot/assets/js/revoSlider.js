var slides = document.querySelectorAll('.slide');
var kutular = document.querySelectorAll('.kutu');
var descList = document.querySelectorAll('.boxDesc');

var currentSlide = 0;
var slideInterval = setInterval(nextSlide, 15000); // 15 saniyede bir otomatik geçiş
var descInterval;

function nextSlide() {
    slides[currentSlide].classList.remove('revoActive');
    currentSlide = (currentSlide + 1) % slides.length;
    slides[currentSlide].classList.add('revoActive');
    showDesc(currentSlide);
}


function showDesc(slideIndex) {
    clearInterval(descInterval);

    descList.forEach(function (desc, descIndex) {
        if (descIndex === slideIndex) {
            desc.style.opacity = '0';
            desc.style.display = 'block';
            desc.style.tra
            fadeDescIn(desc);
        } else {
            desc.style.display = 'none';
        }
    });
}

function fadeDescIn(desc) {
    var opacity = 0;
    descInterval = setInterval(function () {
        if (opacity < 1) {
            opacity += 0.5;
            desc.style.opacity = opacity;
        } else {
            clearInterval(descInterval);
        }
    }, 100);
}

kutular.forEach(function (kutu, index) {
    kutu.addEventListener('mouseover', function () {
        clearInterval(slideInterval);
        showSlide(index);
    });

    kutu.addEventListener('mouseout', function () {
        slideInterval = setInterval(nextSlide, 5000);
    });
});

function showSlide(index) {
    slides.forEach(function (slide, slideIndex) {
        if (slideIndex === index) {
            slide.classList.add('revoActive');
            showDesc(index);
        } else {
            slide.classList.remove('revoActive');

        }
    });
}
