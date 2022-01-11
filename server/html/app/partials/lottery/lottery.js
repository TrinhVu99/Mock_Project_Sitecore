// header scripts
var lottery = {
    slideImage: function () {
        var delayTime = $('.lottery-container').attr('autoPlayTime');
        // console.log(delayTime);
        var lotterySwiper = new Swiper('.lottery-container', {
            navigation: {
                nextEl: '.swiper-button-next',
                prevEl: '.swiper-button-prev',
            },
            loop: true,
            speed: 800,
            autoplay: {
                delay: delayTime,
                disableOnInteraction: false
            }
        });
    },
}