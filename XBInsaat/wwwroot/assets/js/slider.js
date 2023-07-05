const projectSlide = document.querySelectorAll('.projectSlide');
const projectPrevBtn = document.querySelector('.prev-project-btn');
const projectNextBtn = document.querySelector('.next-project-btn');
let projectCurrentSlide = 0;
let projectSlideInterval;

// İlk slaytı göster
projectSlide[projectCurrentSlide].style.display = 'block';

// Otomatik dönme işlemini başlat
startSlideShow();

// Slayt gösterisini başlatan fonksiyon
function startSlideShow() {
    projectSlideInterval = setInterval(nextSlide, 8000); // Saniyede bir döndürmek için 4000 milisaniye (4 saniye)
}

// Slaytı bir sonraki slayta geçiren fonksiyon
function nextSlide() {
    projectSlide[projectCurrentSlide].style.display = 'none';
    projectCurrentSlide = (projectCurrentSlide + 1) % projectSlide.length;
    projectSlide[projectCurrentSlide].style.display = 'block';
}

// Önceki slayta geç
projectPrevBtn.addEventListener('click', function () {
    projectSlide[projectCurrentSlide].style.display = 'none';
    projectCurrentSlide = (projectCurrentSlide - 1 + projectSlide.length) % projectSlide.length;
    projectSlide[projectCurrentSlide].style.display = 'block';
    clearInterval(projectSlideInterval); // Ok tuşlarına tıklandığında otomatik dönme işlemini durdur
});

// Sonraki slayta geç
projectNextBtn.addEventListener('click', function () {
    projectSlide[projectCurrentSlide].style.display = 'none';
    projectCurrentSlide = (projectCurrentSlide + 1) % projectSlide.length;
    projectSlide[projectCurrentSlide].style.display = 'block';
    clearInterval(projectSlideInterval); // Ok tuşlarına tıklandığında otomatik dönme işlemini durdur
});

// Resimler varsa ok tuşlarını gizle
if (projectSlide.length <= 1) {
    projectPrevBtn.style.display = 'none';
    projectNextBtn.style.display = 'none';
}

