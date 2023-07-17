
//----------
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
    newsSlideInterval = setInterval(nextSlide, 3000); // Saniyede bir döndürmek için 4000 milisaniye (4 saniye)
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
                //---------




//var links = document.querySelectorAll('.newModalLink');

//// Her bir link için bir döngü oluşturun
//links.forEach(function (link) {
//    // Her bir linkin tıklama olayını dinleyin
//    link.addEventListener('click', function (e) {

//        // data-value özelliğinden id değerini alın
//        var idA = parseInt(this.getAttribute('data-value'));

//        var languageSpan = document.getElementById("langText");

//        var divElement = document.querySelector(".newJsonModal");

//        divElement.id = idA

//        var modal = document.getElementById(idA);
//        var newTextId = document.getElementById("newTextId");

//        var bootstrapModal = new bootstrap.Modal(modal);

//        var h1ler = document.querySelectorAll('.newModalTitleJs');

//        fetch('/Home/GetNewsJsonData?newItemId=' + idA + '&language=' + languageSpan.innerHTML)
//            .then(response => response.json())
//            .then(jsonData => {
//                // JSON veriyi JavaScript nesnesine dönüştürün
//                var data = JSON.parse(jsonData);
//                h1ler.forEach(x => x.innerHTML = data.Title);
//                newTextId.innerHTML = data.Text;
//                var dispText = ' ';
//                $('#newSliders').html("");

//                for (var i = 0; i < data.NewsImages.length; i++) {
//                    if (i == 0) {
//                        dispText = 'style="display:block"';
//                    }
//                    else {
//                        dispText = style = "";

//                    }
//                    let newsForSlide = `<div class="newSlide" ` + dispText + `><div class="image-container">
//                                                                                                                 <a target="_blank" href="`+ data.InstagramUrl + `">
//                                                                                                                 <img src="./uploads/news/`+ data.NewsImages[i].ImageUrl + `"class="img-fluid rounded hover-lift-light" alt=""></a>
//                                                                                                                 </div></div>`
//                    $('#newSliders').append($(newsForSlide));

//                };
//                let nextNewsBtns = '<a class="prev-new-btn prev-btn" href = "#" >&lt;</a> <a class="next-new-btn next-btn" href = "#" >&gt; </a>'
//                $('#newSliders').append($(nextNewsBtns));

//                // Veriyi div içerisindeki etiketlere uygulayın
//                bootstrapModal.show();
//                //----------
//                const newSlide = document.querySelectorAll('.newSlide');
//                const newPrevBtn = document.querySelector('.prev-new-btn');
//                const newNextBtn = document.querySelector('.next-new-btn');
//                let newCurrentSlide = 0;
//                let newsSlideInterval;

//                // İlk slaytı göster
//                newSlide[newCurrentSlide].style.display = 'block';

//                // Otomatik dönme işlemini başlat
//                startSlideShow();

//                // Slayt gösterisini başlatan fonksiyon
//                function startSlideShow() {
//                    newsSlideInterval = setInterval(nextSlide, 8000); // Saniyede bir döndürmek için 4000 milisaniye (4 saniye)
//                }

//                // Slaytı bir sonraki slayta geçiren fonksiyon
//                function nextSlide() {
//                    newSlide[newCurrentSlide].style.display = 'none';
//                    newCurrentSlide = (newCurrentSlide + 1) % newSlide.length;
//                    newSlide[newCurrentSlide].style.display = 'block';
//                }

//                // Önceki slayta geç
//                newPrevBtn.addEventListener('click', function () {
//                    newSlide[newCurrentSlide].style.display = 'none';
//                    newCurrentSlide = (newCurrentSlide - 1 + newSlide.length) % newSlide.length;
//                    newSlide[newCurrentSlide].style.display = 'block';
//                    clearInterval(newsSlideInterval); // Ok tuşlarına tıklandığında otomatik dönme işlemini durdur
//                });

//                // Sonraki slayta geç
//                newNextBtn.addEventListener('click', function () {
//                    newSlide[newCurrentSlide].style.display = 'none';
//                    newCurrentSlide = (newCurrentSlide + 1) % newSlide.length;
//                    newSlide[newCurrentSlide].style.display = 'block';
//                    clearInterval(newsSlideInterval); // Ok tuşlarına tıklandığında otomatik dönme işlemini durdur
//                });

//                // Resimler varsa ok tuşlarını gizle
//                if (newSlide.length <= 1) {
//                    newPrevBtn.style.display = 'none';
//                    newNextBtn.style.display = 'none';
//                }
//                //---------

//            })
//            .catch(error => {
//                // Hata işleme
//            });
//    });
//});



                    //const newSlide = document.querySelectorAll('.newSlide');
//const newPrevBtn = document.querySelector('.prev-new-btn');
//const newNextBtn = document.querySelector('.next-new-btn');
//let newCurrentSlide = 0;
//let newsSlideInterval;

// İlk slaytı göster
//newSlide[newCurrentSlide].style.display = 'block';

// Otomatik dönme işlemini başlat
//startSlideShow();

// Slayt gösterisini başlatan fonksiyon
//function startSlideShow() {
//    newsSlideInterval = setInterval(nextSlide, 8000); // Saniyede bir döndürmek için 4000 milisaniye (4 saniye)
//}

// Slaytı bir sonraki slayta geçiren fonksiyon
//function nextSlide() {
//    newSlide[newCurrentSlide].style.display = 'none';
//    newCurrentSlide = (newCurrentSlide + 1) % newSlide.length;
//    newSlide[newCurrentSlide].style.display = 'block';
//}

// Önceki slayta geç
//newPrevBtn.addEventListener('click', function () {
//    newSlide[newCurrentSlide].style.display = 'none';
//    newCurrentSlide = (newCurrentSlide - 1 + newSlide.length) % newSlide.length;
//    newSlide[newCurrentSlide].style.display = 'block';
//    clearInterval(newsSlideInterval); // Ok tuşlarına tıklandığında otomatik dönme işlemini durdur
//});

// Sonraki slayta geç
//newNextBtn.addEventListener('click', function () {
//    newSlide[newCurrentSlide].style.display = 'none';
//    newCurrentSlide = (newCurrentSlide + 1) % newSlide.length;
//    newSlide[newCurrentSlide].style.display = 'block';
//    clearInterval(newsSlideInterval); // Ok tuşlarına tıklandığında otomatik dönme işlemini durdur
//});

// Resimler varsa ok tuşlarını gizle
//if (newSlide.length <= 1) {
//    newPrevBtn.style.display = 'none';
//    newNextBtn.style.display = 'none';
//}