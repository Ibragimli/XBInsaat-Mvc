const slidesInfo = document.querySelectorAll('.projectInfoSlide');
const projectInfoPrevBtn = document.querySelector('.info-prev-btn');
const projectInfoNextBtn = document.querySelector('.info-next-btn');
let currentInfoSlide = 0;
let slideInfoInterval;

// İlk slaytı göster
slidesInfo[currentInfoSlide].style.display = 'block';

// Otomatik dönme işlemini başlat
startSlideShow();

// Slayt gösterisini başlatan fonksiyon
function startSlideShow() {
    slideInfoInterval = setInterval(nextSlide, 8000); // Saniyede bir döndürmek için 4000 milisaniye (4 saniye)
}

// Slaytı bir sonraki slayta geçiren fonksiyon
function nextSlide() {
    slidesInfo[currentInfoSlide].style.display = 'none';
    currentInfoSlide = (currentInfoSlide + 1) % slidesInfo.length;
    slidesInfo[currentInfoSlide].style.display = 'block';
}

// Önceki slayta geç
projectInfoPrevBtn.addEventListener('click', function () {
    slidesInfo[currentInfoSlide].style.display = 'none';
    currentInfoSlide = (currentInfoSlide - 1 + slidesInfo.length) % slidesInfo.length;
    slidesInfo[currentInfoSlide].style.display = 'block';
    clearInterval(slideInfoInterval); // Ok tuşlarına tıklandığında otomatik dönme işlemini durdur
});

// Sonraki slayta geç
projectInfoNextBtn.addEventListener('click', function () {
    slidesInfo[currentInfoSlide].style.display = 'none';
    currentInfoSlide = (currentInfoSlide + 1) % slidesInfo.length;
    slidesInfo[currentInfoSlide].style.display = 'block';
    clearInterval(slideInfoInterval); // Ok tuşlarına tıklandığında otomatik dönme işlemini durdur
});

// Resimler varsa ok tuşlarını gizle
if (slidesInfo.length <= 1) {
    projectInfoPrevBtn.style.display = 'none';
    projectInfoNextBtn.style.display = 'none';
}

