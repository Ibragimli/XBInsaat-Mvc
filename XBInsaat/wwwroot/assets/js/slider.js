
$(document).ready(function () {
    const projectSlide = $('.projectSlide');
    const projectPrevBtn = $('.prev-project-btn');
    const projectNextBtn = $('.next-project-btn');
    let projectCurrentSlide = 0;
    let projectsSlideInterval;
    let currentVideo;
    let videoEnded = true;

    // İlk slaytı göster
    projectSlide.eq(projectCurrentSlide).css('display', 'block');

    // Otomatik dönme işlemini başlat
    startSlideShow();

    // Slayt gösterisini başlatan fonksiyon
    function startSlideShow() {
        projectsSlideInterval = setInterval(nextSlide, 3000); // Saniyede bir döndürmek için 8000 milisaniye (8 saniye)
    }

    // Slaytı bir sonraki slayta geçiren fonksiyon
    function nextSlide() {
        if (videoEnded) {
            pauseCurrentVideo();
            projectSlide.eq(projectCurrentSlide).css('display', 'none');
            projectCurrentSlide = (projectCurrentSlide + 1) % projectSlide.length;
            projectSlide.eq(projectCurrentSlide).css('display', 'block');
            playCurrentVideo();
        }
    }

    // Önceki slayta geç
    projectPrevBtn.on('click', function () {
        if (videoEnded) {
            pauseCurrentVideo();
            projectSlide.eq(projectCurrentSlide).css('display', 'none');
            projectCurrentSlide = (projectCurrentSlide - 1 + projectSlide.length) % projectSlide.length;
            projectSlide.eq(projectCurrentSlide).css('display', 'block');
            playCurrentVideo();
            clearInterval(projectsSlideInterval); // Ok tuşlarına tıklandığında otomatik dönme işlemini durdur
            startSlideShow(); // Yeniden döngüyü başlat
        }
    });

    // Sonraki slayta geç
    projectNextBtn.on('click', function () {
        if (videoEnded) {
            pauseCurrentVideo();
            projectSlide.eq(projectCurrentSlide).css('display', 'none');
            projectCurrentSlide = (projectCurrentSlide + 1) % projectSlide.length;
            projectSlide.eq(projectCurrentSlide).css('display', 'block');
            playCurrentVideo();
            clearInterval(projectsSlideInterval); // Ok tuşlarına tıklandığında otomatik dönme işlemini durdur
            startSlideShow(); // Yeniden döngüyü başlat
        }
    });

    // Şu anki slayttaki videoları duraklatır
    function pauseCurrentVideo() {
        currentVideo = projectSlide.eq(projectCurrentSlide).find('.video')[0];
        if (currentVideo) {
            currentVideo.pause();
        }
    }

    // Şu anki slayttaki videoları oynatır
    function playCurrentVideo() {
        currentVideo = projectSlide.eq(projectCurrentSlide).find('.video')[0];
        if (currentVideo) {
            videoEnded = false;
            currentVideo.play();
            currentVideo.onended = function () {
                videoEnded = true;
            };
        }
    }

    // Resimler ve videolar varsa ok tuşlarını gizle
    if (projectSlide.length <= 1) {
        projectPrevBtn.css('display', 'none');
        projectNextBtn.css('display', 'none');
    }
});


//var linksProject = document.querySelectorAll('.projectModalLink');

//// Her bir link için bir döngü oluşturun
//linksProject.forEach(function (linkProject) {
//    // Her bir linkin tıklama olayını dinleyin
//    linkProject.addEventListener('click', function (e) {
//        // data-value özelliğinden id değerini alın
//        var idProject = parseInt(this.getAttribute('data-value'));

//        var languageProjectSpan = document.getElementById("langText");

//        var divElementProject = document.querySelector(".projectJsonModal");

//        divElementProject.id = idProject
//        var modalProject = document.getElementById(idProject);
//        var bootstrapProjectModal = new bootstrap.Modal(modalProject);

//        var projectTextId = document.getElementById("projectTextId");
//        var projectTitle = document.querySelectorAll('.projectModalTitleJs');

//        fetch('/Home/GetProjectsJsonData?projectItemId=' + idProject + '&language=' + languageProjectSpan.innerHTML)
//            .then(response => response.json())
//            .then(jsonData => {
//                // JSON veriyi JavaScript nesnesine dönüştürün
//                var data = JSON.parse(jsonData);
//                projectTitle.forEach(x => x.innerHTML = data.Name);
//                projectTextId.innerHTML = data.Describe;
//                var dispText = ' ';
//                $('#projectSliders').html("");

//                for (var i = 0; i < data.HighProjectImages.length; i++) {
//                    if (i == 0) { dispText = 'style="display:block"'; }
//                    else { dispText = style = ""; }
//                    var divImage = '';

//                    if (data.HighProjectImages[i].ImageTpye == "mp4") {
//                        divImage = `<div class="video-container image-container">
//                                        <video style="width: 100%; height: auto; object-fit: contain;"  class="video" src="./uploads/highprojects/`+ data.HighProjectImages[i].ImageUrl + `"  ></video>
//                                 </div>`;

//                    }
//                    else {
//                        divImage = `<div class="image-container">
//                                    <a  href="#!"> <img src="./uploads/highprojects/`+ data.HighProjectImages[i].ImageUrl + `"class="img-fluid rounded hover-lift-light" alt=""></a>
//                                 </div>`;

//                    }
//                    let projectsForSlide = `<div class="projectSlide" ` + dispText + `>` + divImage + ` </div>`
//                    $('#projectSliders').append($(projectsForSlide));
//                };

//                let nextProjectsBtns = '<a class="prev-project-btn prev-btn" href = "#" >&lt;</a> <a class="next-project-btn next-btn" href = "#" >&gt; </a>'
//                $('#projectSliders').append($(nextProjectsBtns));


//                $('#projectShopsWeb').html("");

//                for (var i = 0; i < data.MidProjects.length; i++) {
//                    let midProjectBox = `  <div class="col-lg-3  col-md-7 mb-2 projectShopBoxs">
//                        <!-- Featured image -->
//                        <a  class="midProjectModalLink" data-value="`+ data.MidProjects[i].MidProjectId + `" href="#!">
//                            <img src="./uploads/midprojects/`+ data.MidProjects[i].MidImageUrl + `"
//                                class="img-fluid rounded hover-lift-light" alt="">
//                        </a>
//                        <!-- Title -->
//                        <a href="#!" data-value="`+ data.MidProjects[i].MidProjectId + `"
//                            class="projectBoxTitle d-block  text-decoration-none mt-3 midProjectModalLink">
//                            `+ data.MidProjects[i].MidProjectName + `
//                        </a>
//                    </div>`
//                    $('#projectShopsWeb').append($(midProjectBox));

//                }
//                $('#projectShopsMobile').html("");

//                for (var i = 0; i < data.MidProjects.length; i++) {
//                    let midProjectMobileBox = `<div class="item me-2">
//                                        <a href="#" data-value="`+ data.MidProjects[i].MidProjectId + `" class="projectBoxTitle d-block  text-decoration-none mt-3 midProjectModalLink">
//                                            <img style=" border-radius: 5px;" src="./uploads/midprojects/`+ data.MidProjects[i].MidImageUrl + `" alt="" width="100% ">
//                                        </a>
//                                        <a href="#!" data-value="`+ data.MidProjects[i].MidProjectId + `"  class="projectBoxTitle d-block  text-decoration-none mt-3 midProjectModalLink" style="color: #000;">` + data.MidProjects[i].MidProjectName + ` </a>
//                                    </div>`
//                    $('#projectShopsMobile').append($(midProjectMobileBox));
//                }


//                // Veriyi div içerisindeki etiketlere uygulayın
//                bootstrapProjectModal.show();

//                //----------
//                $(document).ready(function () {
//                    const projectSlide = $('.projectSlide');
//                    const projectPrevBtn = $('.prev-project-btn');
//                    const projectNextBtn = $('.next-project-btn');
//                    let projectCurrentSlide = 0;
//                    let projectsSlideInterval;
//                    let currentVideo;
//                    let videoEnded = true;

//                    // İlk slaytı göster
//                    projectSlide.eq(projectCurrentSlide).css('display', 'block');

//                    // Otomatik dönme işlemini başlat
//                    startSlideShow();

//                    // Slayt gösterisini başlatan fonksiyon
//                    function startSlideShow() {
//                        projectsSlideInterval = setInterval(nextSlide, 8000); // Saniyede bir döndürmek için 8000 milisaniye (8 saniye)
//                    }

//                    // Slaytı bir sonraki slayta geçiren fonksiyon
//                    function nextSlide() {
//                        if (videoEnded) {
//                            pauseCurrentVideo();
//                            projectSlide.eq(projectCurrentSlide).css('display', 'none');
//                            projectCurrentSlide = (projectCurrentSlide + 1) % projectSlide.length;
//                            projectSlide.eq(projectCurrentSlide).css('display', 'block');
//                            playCurrentVideo();
//                        }
//                    }

//                    // Önceki slayta geç
//                    projectPrevBtn.on('click', function () {
//                        if (videoEnded) {
//                            pauseCurrentVideo();
//                            projectSlide.eq(projectCurrentSlide).css('display', 'none');
//                            projectCurrentSlide = (projectCurrentSlide - 1 + projectSlide.length) % projectSlide.length;
//                            projectSlide.eq(projectCurrentSlide).css('display', 'block');
//                            playCurrentVideo();
//                            clearInterval(projectsSlideInterval); // Ok tuşlarına tıklandığında otomatik dönme işlemini durdur
//                            startSlideShow(); // Yeniden döngüyü başlat
//                        }
//                    });

//                    // Sonraki slayta geç
//                    projectNextBtn.on('click', function () {
//                        if (videoEnded) {
//                            pauseCurrentVideo();
//                            projectSlide.eq(projectCurrentSlide).css('display', 'none');
//                            projectCurrentSlide = (projectCurrentSlide + 1) % projectSlide.length;
//                            projectSlide.eq(projectCurrentSlide).css('display', 'block');
//                            playCurrentVideo();
//                            clearInterval(projectsSlideInterval); // Ok tuşlarına tıklandığında otomatik dönme işlemini durdur
//                            startSlideShow(); // Yeniden döngüyü başlat
//                        }
//                    });

//                    // Şu anki slayttaki videoları duraklatır
//                    function pauseCurrentVideo() {
//                        currentVideo = projectSlide.eq(projectCurrentSlide).find('.video')[0];
//                        if (currentVideo) {
//                            currentVideo.pause();
//                        }
//                    }

//                    // Şu anki slayttaki videoları oynatır
//                    function playCurrentVideo() {
//                        currentVideo = projectSlide.eq(projectCurrentSlide).find('.video')[0];
//                        if (currentVideo) {
//                            videoEnded = false;
//                            currentVideo.play();
//                            currentVideo.onended = function () {
//                                videoEnded = true;
//                            };
//                        }
//                    }

//                    // Resimler ve videolar varsa ok tuşlarını gizle
//                    if (projectSlide.length <= 1) {
//                        projectPrevBtn.css('display', 'none');
//                        projectNextBtn.css('display', 'none');
//                    }
//                });


//                //---------


//                //midProjectJS
//                $(".midProjectModalLink").click(function () {
//                    var idMidProject = parseInt(this.getAttribute('data-value'));
//                    var languageMidProjectSpan = document.getElementById("langText");

//                    var divElementMidProject = document.querySelector(".midProjectJsonModal");

//                    divElementMidProject.id = idMidProject

//                    var modalMidProject = document.getElementById(idMidProject);

//                    var bootstrapMidProjectModal = new bootstrap.Modal(modalMidProject);

//                    var midProjectTextId = document.getElementById("midProjectTextId");
//                    var midProjectTitle = document.querySelectorAll('.midProjectModalTitleJs');
//                    fetch('/Home/GetMidProjectsJsonData?midProjectItemId=' + idMidProject + '&language=' + languageMidProjectSpan.innerHTML)
//                        .then(response => response.json())
//                        .then(jsonData => {
//                            // JSON veriyi JavaScript nesnesine dönüştürün
//                            var data = JSON.parse(jsonData);
//                            midProjectTitle.forEach(x => x.innerHTML = data.Name);
//                            midProjectTextId.innerHTML = data.Describe;
//                            var dispText = ' ';
//                            $('#projectInfoSliders').html("");

//                            for (var i = 0; i < data.MidProjectImages.length; i++) {
//                                if (i == 0) { dispText = 'style="display:block"'; }
//                                else { dispText = style = ""; }
//                                var divMidImage = '';

//                                if (data.MidProjectImages[i].ImageTpye == "mp4") {
//                                    divMidImage = `<div class="video-container image-container">
//                                        <video style="width: 100%; height: auto; object-fit: contain;"  class="video" src="./uploads/midprojects/`+ data.MidProjectImages[i].ImageUrl + `"  ></video>
//                                 </div>`;

//                                }
//                                else {
//                                    divMidImage = `<div class="image-container">
//                                    <a  href="#!"> <img src="./uploads/midprojects/`+ data.MidProjectImages[i].ImageUrl + `"class="img-fluid rounded hover-lift-light" alt=""></a>
//                                 </div>`;

//                                }
//                                console.log(data.MidProjectImages[i].ImageTpye);

//                                let midProjectsForSlide = `<div class="projectInfoSlide" ` + dispText + `> ` + divMidImage + `</div>`
//                                $('#projectInfoSliders').append($(midProjectsForSlide));
//                            };

//                            let nextProjectsBtns = '<a class="info-prev-btn prev-btn" href = "#" >&lt;</a> <a class="info-next-btn next-btn" href = "#" >&gt; </a>'
//                            $('#projectInfoSliders').append($(nextProjectsBtns));


//                            // Veriyi div içerisindeki etiketlere uygulayın
//                            bootstrapMidProjectModal.show();

//                            //----------

//                            $(document).ready(function () {
//                                const slidesInfo = $('.projectInfoSlide');
//                                const projectInfoPrevBtn = $('.info-prev-btn');
//                                const projectInfoNextBtn = $('.info-next-btn');
//                                let slideInfoInterval;
//                                let currentVideo;
//                                let videoEnded = true;
//                                let currentInfoSlide = 0;
//                                // İlk slaytı göster
//                                slidesInfo.eq(currentInfoSlide).css('display', 'block');

//                                // Otomatik dönme işlemini başlat
//                                startSlideShow();

//                                // Slayt gösterisini başlatan fonksiyon
//                                function startSlideShow() {
//                                    slideInfoInterval = setInterval(nextSlide, 8000); // Saniyede bir döndürmek için 8000 milisaniye (8 saniye)
//                                }

//                                // Slaytı bir sonraki slayta geçiren fonksiyon
//                                function nextSlide() {
//                                    if (videoEnded) {
//                                        pauseCurrentVideo();
//                                        slidesInfo.eq(currentInfoSlide).css('display', 'none');
//                                        currentInfoSlide = (currentInfoSlide + 1) % slidesInfo.length;
//                                        slidesInfo.eq(currentInfoSlide).css('display', 'block');
//                                        playCurrentVideo();
//                                    }
//                                }

//                                // Önceki slayta geç
//                                projectInfoPrevBtn.on('click', function () {
//                                    if (videoEnded) {
//                                        pauseCurrentVideo();
//                                        slidesInfo.eq(currentInfoSlide).css('display', 'none');
//                                        currentInfoSlide = (currentInfoSlide - 1 + slidesInfo.length) % slidesInfo.length;
//                                        slidesInfo.eq(currentInfoSlide).css('display', 'block');
//                                        playCurrentVideo();
//                                        clearInterval(slideInfoInterval); // Ok tuşlarına tıklandığında otomatik dönme işlemini durdur
//                                        startSlideShow(); // Yeniden döngüyü başlat
//                                    }
//                                });

//                                // Sonraki slayta geç
//                                projectInfoNextBtn.on('click', function () {
//                                    if (videoEnded) {
//                                        pauseCurrentVideo();
//                                        slidesInfo.eq(currentInfoSlide).css('display', 'none');
//                                        currentInfoSlide = (currentInfoSlide + 1) % slidesInfo.length;
//                                        slidesInfo.eq(currentInfoSlide).css('display', 'block');
//                                        playCurrentVideo();
//                                        clearInterval(slideInfoInterval); // Ok tuşlarına tıklandığında otomatik dönme işlemini durdur
//                                        startSlideShow(); // Yeniden döngüyü başlat
//                                    }
//                                });

//                                // Şu anki slayttaki videoları duraklatır
//                                function pauseCurrentVideo() {
//                                    currentVideo = slidesInfo.eq(currentInfoSlide).find('.video')[0];
//                                    if (currentVideo) {
//                                        currentVideo.pause();
//                                    }
//                                }

//                                // Şu anki slayttaki videoları oynatır
//                                function playCurrentVideo() {
//                                    currentVideo = slidesInfo.eq(currentInfoSlide).find('.video')[0];
//                                    if (currentVideo) {
//                                        videoEnded = false;
//                                        currentVideo.play();
//                                        currentVideo.onended = function () {
//                                            videoEnded = true;
//                                        };
//                                    }
//                                }

//                                // Resimler ve videolar varsa ok tuşlarını gizle
//                                if (slidesInfo.length <= 1) {
//                                    projectInfoPrevBtn.css('display', 'none');
//                                    projectInfoNextBtn.css('display', 'none');
//                                }
//                            });
//                        })
//                        .catch(error => {
//                            // Hata işleme
//                        });
//                });
//            })
//            .catch(error => {
//                // Hata işleme
//            });
//    });
//});









//const projectSlide = document.querySelectorAll('.projectSlide');
//const projectPrevBtn = document.querySelector('.prev-project-btn');
//const projectNextBtn = document.querySelector('.next-project-btn');
//let projectCurrentSlide = 0;
//let projectSlideInterval;

// İlk slaytı göster
//projectSlide[projectCurrentSlide].style.display = 'block';

// Otomatik dönme işlemini başlat
//startSlideShow();

// Slayt gösterisini başlatan fonksiyon
//function startSlideShow() {
//    projectSlideInterval = setInterval(nextSlide, 8000); // Saniyede bir döndürmek için 4000 milisaniye (4 saniye)
//}

// Slaytı bir sonraki slayta geçiren fonksiyon
//function nextSlide() {
//    projectSlide[projectCurrentSlide].style.display = 'none';
//    projectCurrentSlide = (projectCurrentSlide + 1) % projectSlide.length;
//    projectSlide[projectCurrentSlide].style.display = 'block';
//}

// Önceki slayta geç
//projectPrevBtn.addEventListener('click', function () {
//    projectSlide[projectCurrentSlide].style.display = 'none';
//    projectCurrentSlide = (projectCurrentSlide - 1 + projectSlide.length) % projectSlide.length;
//    projectSlide[projectCurrentSlide].style.display = 'block';
//    clearInterval(projectSlideInterval); // Ok tuşlarına tıklandığında otomatik dönme işlemini durdur
//});

// Sonraki slayta geç
//projectNextBtn.addEventListener('click', function () {
//    projectSlide[projectCurrentSlide].style.display = 'none';
//    projectCurrentSlide = (projectCurrentSlide + 1) % projectSlide.length;
//    projectSlide[projectCurrentSlide].style.display = 'block';
//    clearInterval(projectSlideInterval); // Ok tuşlarına tıklandığında otomatik dönme işlemini durdur
//});

// Resimler varsa ok tuşlarını gizle
//if (projectSlide.length <= 1) {
//    projectPrevBtn.style.display = 'none';
//    projectNextBtn.style.display = 'none';
//}

