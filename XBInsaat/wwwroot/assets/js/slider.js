
var linksProject = document.querySelectorAll('.projectModalLink');

// Her bir link için bir döngü oluşturun
linksProject.forEach(function (linkProject) {
    // Her bir linkin tıklama olayını dinleyin
    linkProject.addEventListener('click', function (e) {
        // data-value özelliğinden id değerini alın
        var idProject = parseInt(this.getAttribute('data-value'));

        var languageProjectSpan = document.getElementById("langText");

        var divElementProject = document.querySelector(".projectJsonModal");

        divElementProject.id = idProject
        var modalProject = document.getElementById(idProject);
        var bootstrapProjectModal = new bootstrap.Modal(modalProject);

        var projectTextId = document.getElementById("projectTextId");
        var projectTitle = document.querySelectorAll('.projectModalTitleJs');

        fetch('/Home/GetProjectsJsonData?projectItemId=' + idProject + '&language=' + languageProjectSpan.innerHTML)
            .then(response => response.json())
            .then(jsonData => {
                // JSON veriyi JavaScript nesnesine dönüştürün
                var data = JSON.parse(jsonData);
                projectTitle.forEach(x => x.innerHTML = data.Name);
                projectTextId.innerHTML = data.Describe;
                var dispText = ' ';
                $('#projectSliders').html("");

                for (var i = 0; i < data.HighProjectImages.length; i++) {
                    if (i == 0) { dispText = 'style="display:block"'; }
                    else { dispText = style = ""; }

                    console.log(data.HighProjectImages[i].ImageUrl)
                    let projectsForSlide = `<div class="projectSlide" ` + dispText + `><div class="image-container">
                        <a  href="#!">
                        <img src="./uploads/highprojects/`+ data.HighProjectImages[i].ImageUrl + `"class="img-fluid rounded hover-lift-light" alt=""></a>
                        </div></div>`
                    $('#projectSliders').append($(projectsForSlide));
                };
                let nextProjectsBtns = '<a class="prev-project-btn prev-btn" href = "#" >&lt;</a> <a class="next-project-btn next-btn" href = "#" >&gt; </a>'
                $('#projectSliders').append($(nextProjectsBtns));


                $('#projectShopsWeb').html("");

                for (var i = 0; i < data.MidProjects.length; i++) {
                    let midProjectBox = `  <div class="col-lg-3  col-md-7 mb-2 projectShopBoxs">
                        <!-- Featured image -->
                        <a  class="midProjectModalLink" data-value="`+ data.MidProjects[i].MidProjectId + `" href="#!">
                            <img src="./uploads/midprojects/`+ data.MidProjects[i].MidImageUrl + `"
                                class="img-fluid rounded hover-lift-light" alt="">
                        </a>
                        <!-- Title -->
                        <a href="#!" data-value="`+ data.MidProjects[i].MidProjectId + `"
                            class="projectBoxTitle d-block  text-decoration-none mt-3 midProjectModalLink">
                            `+ data.MidProjects[i].MidProjectName + `
                        </a>
                    </div>`
                    $('#projectShopsWeb').append($(midProjectBox));

                }
                $('#projectShopsMobile').html("");

                for (var i = 0; i < data.MidProjects.length; i++) {
                    let midProjectMobileBox = `<div class="item me-2">
                                        <a href="#" data-value="`+ data.MidProjects[i].MidProjectId + `" class="projectBoxTitle d-block  text-decoration-none mt-3 midProjectModalLink">
                                            <img style=" border-radius: 5px;" src="./uploads/midprojects/`+ data.MidProjects[i].MidImageUrl + `" alt="" width="100% ">
                                        </a>
                                        <a href="#!" data-value="`+ data.MidProjects[i].MidProjectId + `"  class="projectBoxTitle d-block  text-decoration-none mt-3 midProjectModalLink" style="color: #000;">` + data.MidProjects[i].MidProjectName + ` </a>
                                    </div>`
                    $('#projectShopsMobile').append($(midProjectMobileBox));
                }


                // Veriyi div içerisindeki etiketlere uygulayın
                bootstrapProjectModal.show();

                //----------
                const projectSlide = document.querySelectorAll('.projectSlide');
                const projectPrevBtn = document.querySelector('.prev-project-btn');
                const projectNextBtn = document.querySelector('.next-project-btn');
                let projectCurrentSlide = 0;
                let projectsSlideInterval;

                // İlk slaytı göster
                projectSlide[projectCurrentSlide].style.display = 'block';

                // Otomatik dönme işlemini başlat
                startSlideShow();

                // Slayt gösterisini başlatan fonksiyon
                function startSlideShow() {
                    projectsSlideInterval = setInterval(nextSlide, 8000); // Saniyede bir döndürmek için 4000 milisaniye (4 saniye)
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
                    clearInterval(projectsSlideInterval); // Ok tuşlarına tıklandığında otomatik dönme işlemini durdur
                });

                // Sonraki slayta geç
                projectNextBtn.addEventListener('click', function () {
                    projectSlide[projectCurrentSlide].style.display = 'none';
                    projectCurrentSlide = (projectCurrentSlide + 1) % projectSlide.length;
                    projectSlide[projectCurrentSlide].style.display = 'block';
                    clearInterval(projectsSlideInterval); // Ok tuşlarına tıklandığında otomatik dönme işlemini durdur
                });

                // Resimler varsa ok tuşlarını gizle
                if (projectSlide.length <= 1) {
                    projectPrevBtn.style.display = 'none';
                    projectNextBtn.style.display = 'none';
                }
                //---------


                //midProjectJS
                $(".midProjectModalLink").click(function () {
                    var idMidProject = parseInt(this.getAttribute('data-value'));
                    console.log(idMidProject);
                    var languageMidProjectSpan = document.getElementById("langText");
                    console.log(languageMidProjectSpan);

                    var divElementMidProject = document.querySelector(".midProjectJsonModal");
                    console.log(divElementMidProject);

                    divElementMidProject.id = idMidProject
                    console.log(divElementMidProject.id);

                    var modalMidProject = document.getElementById(idMidProject);
                    console.log(modalMidProject);

                    var bootstrapMidProjectModal = new bootstrap.Modal(modalMidProject);

                    var midProjectTextId = document.getElementById("midProjectTextId");
                    var midProjectTitle = document.querySelectorAll('.midProjectModalTitleJs');
                    fetch('/Home/GetMidProjectsJsonData?midProjectItemId=' + idMidProject + '&language=' + languageMidProjectSpan.innerHTML)
                        .then(response => response.json())
                        .then(jsonData => {
                            // JSON veriyi JavaScript nesnesine dönüştürün
                            var data = JSON.parse(jsonData);
                            midProjectTitle.forEach(x => x.innerHTML = data.Name);
                            midProjectTextId.innerHTML = data.Describe;
                            var dispText = ' ';
                            $('#projectInfoSliders').html("");

                            for (var i = 0; i < data.MidProjectImages.length; i++) {
                                if (i == 0) { dispText = 'style="display:block"'; }
                                else { dispText = style = ""; }

                                let midProjectsForSlide = `<div class="projectInfoSlide" ` + dispText + `><div class="image-container">
                        <a  href="#!">
                        <img src="./uploads/midprojects/`+ data.MidProjectImages[i].ImageUrl + `"class="img-fluid rounded hover-lift-light" alt=""></a>
                        </div></div>`
                                $('#projectInfoSliders').append($(midProjectsForSlide));
                            };

                            let nextProjectsBtns = '<a class="info-prev-btn prev-btn" href = "#" >&lt;</a> <a class="info-next-btn next-btn" href = "#" >&gt; </a>'
                            $('#projectInfoSliders').append($(nextProjectsBtns));


                            // Veriyi div içerisindeki etiketlere uygulayın
                            bootstrapMidProjectModal.show();

                            //----------

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

                            //---------

                        })
                        .catch(error => {
                            // Hata işleme
                        });
                });
            })
            .catch(error => {
                // Hata işleme
            });
    });
});









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

