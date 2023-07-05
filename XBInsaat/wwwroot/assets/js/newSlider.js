const newSlide = document.querySelectorAll('.newSlide');
const newPrevBtn = document.querySelector('.prev-new-btn');
const newNextBtn = document.querySelector('.next-new-btn');
let newCurrentSlide = 0;
let newsSlideInterval;

// İlk slaytı göster
newSlide[newCurrentSlide].style.display = 'block';

// Otomatik dönme işlemini başlat
startSlideShow();

// Slayt gösterisini başlatan fonksiyon
function startSlideShow() {
    newsSlideInterval = setInterval(nextSlide, 8000); // Saniyede bir döndürmek için 4000 milisaniye (4 saniye)
}

// Slaytı bir sonraki slayta geçiren fonksiyon
function nextSlide() {
    newSlide[newCurrentSlide].style.display = 'none';
    newCurrentSlide = (newCurrentSlide + 1) % newSlide.length;
    newSlide[newCurrentSlide].style.display = 'block';
}

// Önceki slayta geç
newPrevBtn.addEventListener('click', function () {
    newSlide[newCurrentSlide].style.display = 'none';
    newCurrentSlide = (newCurrentSlide - 1 + newSlide.length) % newSlide.length;
    newSlide[newCurrentSlide].style.display = 'block';
    clearInterval(newsSlideInterval); // Ok tuşlarına tıklandığında otomatik dönme işlemini durdur
});

// Sonraki slayta geç
newNextBtn.addEventListener('click', function () {
    newSlide[newCurrentSlide].style.display = 'none';
    newCurrentSlide = (newCurrentSlide + 1) % newSlide.length;
    newSlide[newCurrentSlide].style.display = 'block';
    clearInterval(newsSlideInterval); // Ok tuşlarına tıklandığında otomatik dönme işlemini durdur
});

// Resimler varsa ok tuşlarını gizle
if (newSlide.length <= 1) {
    newPrevBtn.style.display = 'none';
    newNextBtn.style.display = 'none';
}

