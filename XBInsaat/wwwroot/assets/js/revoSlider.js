var currentSlide = 0;
var slideInterval = setInterval(nextSlide, 3500);
var descInterval;

function nextSlide() {
    $('.slide').eq(currentSlide).removeClass('revoActive');
    currentSlide = (currentSlide + 1) % $('.slide').length;
    $('.slide').eq(currentSlide).addClass('revoActive');
    showDesc(currentSlide);
}

function showDesc(slideIndex) {
    clearInterval(descInterval);

    $('.boxDesc').each(function (descIndex) {
        if (descIndex === slideIndex) {
            $(this).css('opacity', '0').css('display', 'block');
            fadeDescIn($(this));
        } else {
            $(this).css('display', 'none');
        }
    });
}

function fadeDescIn(desc) {
    var opacity = 0;
    descInterval = setInterval(function () {
        if (opacity < 1) {
            opacity += 0.5;
            desc.css('opacity', opacity);
        } else {
            clearInterval(descInterval);
        }
    }, 100);
}

$('.kutu').each(function (index) {
    $(this).mouseover(function () {
        clearInterval(slideInterval);
        currentSlide = index;
        showSlide(index);
    });
 


    $(this).mouseout(function () {
        slideInterval = setInterval(nextSlide, 3500);

    });
});

function showSlide(index) {
    $('.slide').each(function (slideIndex) {
        if (slideIndex === index) {
            $(this).addClass('revoActive');
            showDesc(index);
        } else {
            $(this).removeClass('revoActive');
        }
    });
}




