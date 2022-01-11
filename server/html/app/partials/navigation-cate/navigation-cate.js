var navCate = {
    initNavCateSwiper: function () {
        if ($('.nav-cate').length) {
            var activeSlideIndex = 0;
            $('.nav-cate-list .nav-cate-item').each(function () {
                if ($(this).hasClass('active')) {
                    activeSlideIndex = $(this).index();
                }
            });
            var navCateSwiper = document.querySelector('.nav-cate').swiper;
            if (navCateSwiper) {
                navCateSwiper.destroy(false, false);
            }
            var optionsSwiper = {
                init: true,
                slidesPerView: 'auto',
                spaceBetween: 2,
                noSwiping: false,
                initialSlide: activeSlideIndex,
                noSwipingClass: 'swiper-no-swiping'
            };
            if ($(window).width() >= 768) {
                optionsSwiper.navigation = {
                    nextEl: '.button__swiper.is-next',
                    prevEl: '.button__swiper.is-prev',
                }

                optionsSwiper.on = {
                    reachBeginning: function () {
                        $('.button__swiper.is-prev').css('display', 'none');
                        $('.button__swiper.is-next').css('display', 'flex');
                        // navCateSwiper.setTranslate('translate3d(100%, 0px, 0px)');
                    },
                    reachEnd: function () {
                        $('.button__swiper.is-prev').css('display', 'flex');
                        $('.button__swiper.is-next').css('display', 'none');
                        // navCateSwiper.setTranslate('translate3d(-100%, 0px, 0px)');
                    }
                }
            } else {
                $('.button__swiper.is-prev').css('display', 'none');
                $('.button__swiper.is-next').css('display', 'none');
            }
            navCateSwiper = new Swiper('.nav-cate', optionsSwiper);

            // if ($(window).width() >= 768) {
            //     navCate.detectShowHideArrowOnChange(navCateSwiper);
            // }

        }
    },
    // detectShowHideArrowOnChange: function name(navCateSwiper) {
    //     navCateSwiper.on('slideChange', function () {
    //         navCate.detectShowHideArrowOnInit(navCateSwiper);
    //     });
    // },
    // detectShowHideArrowOnInit: function (navCateSwiper) {
    //     var navCateWrapperWidth = $('.nav-cate-list').width();
    //     var navCateItemWidth = $('.nav-cate-item').width();
    //     var navCateItemOnShow = Math.ceil(navCateWrapperWidth / navCateItemWidth) - 1;
    //     console.log('navCateItemOnShow', navCateItemOnShow);
    //     console.log(' navCateSwiper.slides.length - navCateItemOnShow', navCateSwiper.slides.length - navCateItemOnShow);
    //     if (navCateSwiper.activeIndex == 0) {
    //         $('.button__swiper.is-prev').css('display', 'none');
    //         $('.button__swiper.is-next').css('display', 'flex');
    //     }
    //     // most right postion
    //     else if (navCateSwiper.activeIndex == navCateSwiper.slides.length - navCateItemOnShow) {
    //         $('.button__swiper.is-prev').css('display', 'flex');
    //         $('.button__swiper.is-next').css('display', 'none');
    //     }
    //     // middle positions
    //     else {
    //         $('.button__swiper.is-prev').css('display', 'flex');
    //         $('.button__swiper.is-next').css('display', 'flex');
    //     }
    // }
}
