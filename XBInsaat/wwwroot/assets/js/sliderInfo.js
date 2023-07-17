$(document).ready(function () {
    const slidesInfo = $('.projectInfoSlide');
    const projectInfoPrevBtn = $('.info-prev-btn');
    const projectInfoNextBtn = $('.info-next-btn');
    let slideInfoInterval;
    let currentVideo;
    let videoEnded = true;
    let currentInfoSlide = 0;
    // İlk slaytı göster
    slidesInfo.eq(currentInfoSlide).css('display', 'block');

    // Otomatik dönme işlemini başlat
    startSlideShow();

    // Slayt gösterisini başlatan fonksiyon
    function startSlideShow() {
        slideInfoInterval = setInterval(nextSlide, 3000); // Saniyede bir döndürmek için 8000 milisaniye (8 saniye)
    }

    // Slaytı bir sonraki slayta geçiren fonksiyon
    function nextSlide() {
        if (videoEnded) {
            pauseCurrentVideo();
            slidesInfo.eq(currentInfoSlide).css('display', 'none');
            currentInfoSlide = (currentInfoSlide + 1) % slidesInfo.length;
            slidesInfo.eq(currentInfoSlide).css('display', 'block');
            playCurrentVideo();
        }
    }

    // Önceki slayta geç
    projectInfoPrevBtn.on('click', function () {
        if (videoEnded) {
            pauseCurrentVideo();
            slidesInfo.eq(currentInfoSlide).css('display', 'none');
            currentInfoSlide = (currentInfoSlide - 1 + slidesInfo.length) % slidesInfo.length;
            slidesInfo.eq(currentInfoSlide).css('display', 'block');
            playCurrentVideo();
            clearInterval(slideInfoInterval); // Ok tuşlarına tıklandığında otomatik dönme işlemini durdur
            startSlideShow(); // Yeniden döngüyü başlat
        }
    });

    // Sonraki slayta geç
    projectInfoNextBtn.on('click', function () {
        if (videoEnded) {
            pauseCurrentVideo();
            slidesInfo.eq(currentInfoSlide).css('display', 'none');
            currentInfoSlide = (currentInfoSlide + 1) % slidesInfo.length;
            slidesInfo.eq(currentInfoSlide).css('display', 'block');
            playCurrentVideo();
            clearInterval(slideInfoInterval); // Ok tuşlarına tıklandığında otomatik dönme işlemini durdur
            startSlideShow(); // Yeniden döngüyü başlat
        }
    });

    // Şu anki slayttaki videoları duraklatır
    function pauseCurrentVideo() {
        currentVideo = slidesInfo.eq(currentInfoSlide).find('.video')[0];
        if (currentVideo) {
            currentVideo.pause();
        }
    }

    // Şu anki slayttaki videoları oynatır
    function playCurrentVideo() {
        currentVideo = slidesInfo.eq(currentInfoSlide).find('.video')[0];
        if (currentVideo) {
            videoEnded = false;
            currentVideo.play();
            currentVideo.onended = function () {
                videoEnded = true;
            };
        }
    }

    // Resimler ve videolar varsa ok tuşlarını gizle
    if (slidesInfo.length <= 1) {
        projectInfoPrevBtn.css('display', 'none');
        projectInfoNextBtn.css('display', 'none');
    }
});


//var linksMidProject = document.querySelectorAll('.midProjectModalLink');
//console.log(linksMidProject)
//// Her bir link için bir döngü oluşturun

//linksMidProject.forEach(function (linkMidProject) {
//    // Her bir linkin tıklama olayını dinleyin

//    linkMidProject.addEventListener('click', function (e) {
//        var idMidProject = parseInt(this.getAttribute('data-value'));
//        console.log(idMidProject);
//        var languageMidProjectSpan = document.getElementById("langText");
//        console.log(languageMidProjectSpan);

//        var divElementMidProject = document.querySelector(".midProjectJsonModal");
//        console.log(divElementMidProject);

//        divElementMidProject.id = idMidProject
//        console.log(divElementMidProject.id);

//        var modalMidProject = document.getElementById(idMidProject);
//        console.log(modalMidProject);

//        var bootstrapMidProjectModal = new bootstrap.Modal(modalMidProject);

//        var midProjectTextId = document.getElementById("midProjectTextId");
//        var midProjectTitle = document.querySelectorAll('.midProjectModalTitleJs');
//        fetch('/Home/GetMidProjectsJsonData?midProjectItemId=' + idMidProject + '&language=' + languageMidProjectSpan.innerHTML)
//            .then(response => response.json())
//            .then(jsonData => {
//                // JSON veriyi JavaScript nesnesine dönüştürün
//                var data = JSON.parse(jsonData);
//                midProjectTitle.forEach(x => x.innerHTML = data.Name);
//                midProjectTextId.innerHTML = data.Describe;
//                var dispText = ' ';
//                $('#projectInfoSliders').html("");

//                for (var i = 0; i < data.MidProjectImages.length; i++) {
//                    if (i == 0) { dispText = 'style="display:block"'; }
//                    else { dispText = style = ""; }

//                    let midProjectsForSlide = `<div class="projectInfoSlide" ` + dispText + `><div class="image-container">
//                        <a  href="#!">
//                        <img src="./uploads/midprojects/`+ data.MidProjectImages[i].ImageUrl + `"class="img-fluid rounded hover-lift-light" alt=""></a>
//                        </div></div>`
//                    $('#projectInfoSliders').append($(midProjectsForSlide));
//                };

//                let nextProjectsBtns = '<a class="info-prev-btn prev-btn" href = "#" >&lt;</a> <a class="info-next-btn next-btn" href = "#" >&gt; </a>'
//                $('#projectInfoSliders').append($(nextProjectsBtns));


//                // Veriyi div içerisindeki etiketlere uygulayın
//                bootstrapMidProjectModal.show();

//                //----------

//                const slidesInfo = document.querySelectorAll('.projectInfoSlide');
//                const projectInfoPrevBtn = document.querySelector('.info-prev-btn');
//                const projectInfoNextBtn = document.querySelector('.info-next-btn');
//                let currentInfoSlide = 0;
//                let slideInfoInterval;

//                // İlk slaytı göster
//                slidesInfo[currentInfoSlide].style.display = 'block';

//                // Otomatik dönme işlemini başlat
//                startSlideShow();

//                // Slayt gösterisini başlatan fonksiyon
//                function startSlideShow() {
//                    slideInfoInterval = setInterval(nextSlide, 8000); // Saniyede bir döndürmek için 4000 milisaniye (4 saniye)
//                }

//                // Slaytı bir sonraki slayta geçiren fonksiyon
//                function nextSlide() {
//                    slidesInfo[currentInfoSlide].style.display = 'none';
//                    currentInfoSlide = (currentInfoSlide + 1) % slidesInfo.length;
//                    slidesInfo[currentInfoSlide].style.display = 'block';
//                }

//                // Önceki slayta geç
//                projectInfoPrevBtn.addEventListener('click', function () {
//                    slidesInfo[currentInfoSlide].style.display = 'none';
//                    currentInfoSlide = (currentInfoSlide - 1 + slidesInfo.length) % slidesInfo.length;
//                    slidesInfo[currentInfoSlide].style.display = 'block';
//                    clearInterval(slideInfoInterval); // Ok tuşlarına tıklandığında otomatik dönme işlemini durdur
//                });

//                // Sonraki slayta geç
//                projectInfoNextBtn.addEventListener('click', function () {
//                    slidesInfo[currentInfoSlide].style.display = 'none';
//                    currentInfoSlide = (currentInfoSlide + 1) % slidesInfo.length;
//                    slidesInfo[currentInfoSlide].style.display = 'block';
//                    clearInterval(slideInfoInterval); // Ok tuşlarına tıklandığında otomatik dönme işlemini durdur
//                });

//                // Resimler varsa ok tuşlarını gizle
//                if (slidesInfo.length <= 1) {
//                    projectInfoPrevBtn.style.display = 'none';
//                    projectInfoNextBtn.style.display = 'none';
//                }

//                //---------

//            })
//            .catch(error => {
//                // Hata işleme
//            });
//    });
//});




