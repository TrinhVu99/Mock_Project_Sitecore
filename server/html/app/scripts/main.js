'use strict';
var isDevice = /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent),
    isAndroid = /Android/i.test(navigator.userAgent),
    isIos = /iPhone|iPad|iPod/i.test(navigator.userAgent),
    mobileWidth = 767,
    deviceWidth = 1024,
    isIE11 = !!(navigator.userAgent.match(/Trident/) && navigator.userAgent.match(/rv[ :]11/)),
    isMobile = $(window).width() < 768;
var timing = {
    easingTime: 700,
}
var global = {
    detectDevices: function () {
        var a = isDevice === true ? ' device' : ' pc',
            b = isAndroid === true ? ' android' : ' not-android',
            c = isIos === true ? ' ios' : ' not-ios',
            d = isMobile ? ' mobile' : ' desktop',
            e = isIE11 ? ' ie11' : ' ',
            htmlClass = a + b + c + d + e;
        $('html').addClass(htmlClass);
        if (isDevice) $('body').css('cursor', 'pointer');
    },
    showElement: function () {
        if ($('.main-content .animated').length) {
            $('.main-content .animated').each(function () {
                var viewportBottom = $(window).scrollTop() + $(window).height();
                var elementOffsetTop = $(this).offset().top;
                var elementHeight = $(this).outerHeight();
                var maxOffsetToShowElement = isMobile ? 200 : 400;
                if (viewportBottom > (elementOffsetTop + elementHeight) || viewportBottom > (elementOffsetTop + maxOffsetToShowElement)) $(this).addClass('fadeInUp');
            });
        }
    },
    replaceImgToBackground: function (img) {
        $.each(img, function () {
            if ($(this).css('visibility') == 'visible') {
                $(this).parent().css('background-image', 'url(' + $(this).attr('src') + ')');
            };
        });
    },
    imgCarousel: function (ele) {
        $(ele).each(function (index) {
            if ($(this).length && $(this).children().length) {
                var $this = $(this);
                // $this.appear();
                // $this.on('appear', function (event, $all_appeared_elements) {
                //     $this.addClass('appear');
                //     // console.log($this.find('.swiper-slide-next'));
                // });
                // $this.on('disappear', function (event, $all_disappeared_elements) {
                //     $this.removeClass('appear');
                // });
                var delayTime = $this.attr('autoPlayTime');
                $this.addClass('instance-' + index);
                var swiper = new Swiper('.instance-' + index, {
                    direction: 'horizontal',
                    speed: 800,
                    pagination: {
                        el: $this.find('.swiper-pagination'),
                        clickable: true
                    },
                    loop: true,
                    autoplay: {
                        delay: delayTime,
                        disableOnInteraction: false
                    },
                    fadeEffect: {
                        crossFade: true
                    },
                    on: {
                        slideChange: function () {
                            if ($this.hasClass('appear')) {
                                // console.log($this.find('.swiper-slide-next'));
                                // $this.find('.swiper-slide-active')
                                var requestModel = {
                                    ad_id: $this.find('.swiper-slide-next').attr('ad-id') ? $this.find('.swiper-slide-next').attr('ad-id').slice(1, -1) : null,
                                    path: window.location.pathname,
                                    page_id: $('#page_id').val() ? $('#page_id').val().slice(1, -1) : null
                                };
                                var sectionId = $this.find('.swiper-slide-next').closest('.swiper-container').attr('section-id');
                                var advertiseRootId = requestModel.ad_id ? sectionId.concat(requestModel.ad_id) : null;
                                // console.log(requestModel);
                                $.ajax({
                                    type: 'POST',
                                    url: '/ajax/advertisement/adsImpression',
                                    dataType: 'json',
                                    contentType: 'application/json',
                                    data: JSON.stringify(requestModel),
                                    success: function (result) {
                                        if (result.IsSuccess) {
                                            // sentIdAdvertise.push(requestModel.ad_id);
                                        }
                                    }
                                });
                            }
                        },
                    },
                });
                // $('.advertising-section .swiper-container ').appear();
            }
        });
    },
    responsiveElementDesktop: function () {
        var _w = $(window).width();
        if (_w > 767) {
            if ($('#e-newsletter-form .submit-email-text').length < 1) {
                $('.e-newsletter-subscription-section .submit-email-text').clone().appendTo($('#e-newsletter-form .email-submit-button'));
            }
            if ($('.photo-day-section #lottery-section').length < 1) {
                $('.hero-section #lottery-section').clone().appendTo($('.photo-day-section .lottery-board'));
            }
        }
        if (_w > 1023) {
            if ($('.bloggers-section #photo-of-the-day').length < 1) {
                $('.photo-day-section #photo-of-the-day').clone().appendTo($('.bloggers-section .photo-day-section'));
            }
        }
    },
    overflowBody: function () {
        $('html, body').css({
            height: '100%',
            overflow: 'hidden'
        });
    },
    clearOverflowBody: function () {
        $('html, body').css({
            height: 'auto',
            overflow: 'visible'
        });
    },
    scrollTo: function (offset, easingTime) {
        $('html, body').animate({
            scrollTop: offset
        }, easingTime);
    },
    removeReadmoreButton: function () {
        var fadedSection = $('.fadedSection');
        fadedSection.each(function () {
            if ($(this)[0].scrollHeight <= parseInt($(this).css('max-height'), 10)) {
                $(this).find('.news-detail-readmore-btn').css('opacity', 0);
                $(this).removeClass('active-fade');
            } else {
                $(this).find('.news-detail-readmore-btn').css('opacity', 1);
                $(this).addClass('active-fade');
            };
        });
    },
    removeMaxHeight: function (section) {
        var height = $('body')[0].scrollHeight;
        setTimeout(function () {
            section.css('max-height', height);
        });
        setTimeout(function () {
            section.css('max-height', '100%');
        }, 100);
    },
    breakTextLine: function (section, height) {
        section.dotdotdot({
            // configuration goes here
            ellipsis: '...',
            watch: true,
            wrap: 'letter',
            height: height
        });
    },
    hideEmptySection: function () {
        if ($('.hide-content')) {
            $('.hide-content').each(function () {
                if ($(this).attr('group-name')) {
                    var ele = $(this).attr('group-name');
                    $('[group-name="' + ele + '"]').hide();
                }
            });
        }
    },
	/* highlightSearchPattern: function () {
		var src_str = $('.search-result-item').html();
		var term = $('#keyword').text();
		if ($('.search-result-item').length && $('#keyword').length) {
			term = term.replace(/(\s+)/, '(<[^>]+>)*$1(<[^>]+>)*');
			var pattern = new RegExp('(' + term + ')', 'gi');
			src_str = src_str.replace(pattern, '<mark>$1</mark>');
			src_str = src_str.replace(/(<mark>[^<>]*)((<[^>]+>)+)([^<>]*<\/mark>)/, '$1</mark>$2<mark>$4');
			$('.search-result-item').html(src_str);
		}
	}, */
    onClickEvent: function (ele) {
        $(ele).click(function (e) {
            if ($(this).hasClass('active')) {
                return;
            } else {
                $(ele).removeClass('active');

                $(this).addClass('active');
            }
        });
    },
    scrollToTop: function () {
        if ($('#back-to-top').length) {
            var scrollTrigger = 100;
            var scrollToTop = $(window).scrollTop();
            if (scrollToTop > scrollTrigger) {
                $('#back-to-top').addClass('show');
            } else {
                $('#back-to-top').removeClass('show');
            }
        }
    },
    backToTop: function () {
        if ($('#back-to-top').length) {
            $('#back-to-top').on('click', function (e) {
                e.preventDefault();
                $('html,body').animate({
                    scrollTop: 0
                }, 700);
                return false;
            });
        }
    },
    scrollAdvertise: function () {
        $('.advertising-fixed').each(function () {
            if ($(this).is(':visible') && $(window).width() > 768) {
                $(this).stick_in_parent();
            } else {
                $(this).trigger('sticky_kit:detach');
            }
        });

    },
    clickAds: function (fn) {
        var adsId = $(fn).data('ads-id');
        var requestModel = {
            ad_id: adsId.slice(1, -1),
            path: window.location.pathname,
            page_id: $('#page_id').val().slice(1, -1)
        };
        $.ajax({
            type: 'POST',
            url: '/ajax/advertisement/click',
            data: JSON.stringify(requestModel),
            dataType: 'json',
            contentType: 'application/json',
            success: function (result) {
            }
        });
    },
    init: function () { },
    load: function () { },
    resize: function () { }
}
