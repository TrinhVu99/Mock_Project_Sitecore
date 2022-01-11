$(function () {
    global.showElement();
    global.detectDevices();
    global.replaceImgToBackground($('.img-to-bg img'));
    global.responsiveElementDesktop();
    global.imgCarousel('.banner-carousel, .advertising-carousel, .advertising-in-col');
    global.hideEmptySection();
    hero.openVideoLightbox('youtubeVideo');
    footer.responsiveElement();
    footer.expandMenu();
    footer.subscribeForm();
    lightbox.closeLightbox();
    header.getAutocompleteTerm();
    header.getLocation();
    header.closeMainNav();
    header.openMainNav();
    header.toggleSearchGlobal();
    header.initSearchGlobalValidation();
    header.searchPage();
    header.toggleLang();
    navCate.initNavCateSwiper();
    detailPage.responsiveElement();
    detailPage.moveAdvertisingSection();
    detailPageTitle.likeButton();
    showLessMore.setHeightItemList();
    showLessMore.expandItem();
    showLessMore.collapseItem();
    eventDetailPage.responsiveElement();
    eventDetailPage.showUpcomingSidebar();
    readmoreButton.continueReading();
    global.removeReadmoreButton();
    postLeftImg.breakTextLine();
    postOverlapTitle.breakTextLine();
    video.breakTextLine();
    postLeftImg.breakTextLineSearch();
    global.onClickEvent('.filter-list li');
    global.onClickEvent('.search-result-list .pagination li.page-item');
    global.scrollToTop();
    global.backToTop();
    global.scrollAdvertise();
    detailPageTitle.showSocialIconList();
    detailPageTitle.reponsiveElement();
    header.showSearchBar();
    lottery.slideImage();
    bloggerComponent.showMoreBtn();
    header.showPopUpMenuLink();
    // global.clickAdvertise();
    socialIconColumn.lineShareButton();
});
$(window).on('load', function (e) {
    showLessMore.setHeightItemList();
    header.autocompleteTermScroll();
});
var width = $(window).width();
var resize = 0;
var fullScreenRequest = false;
var isFullscreenNow = false;
$(document).on('webkitfullscreenchange mozfullscreenchange fullscreenchange orientationchange', function (e) {
    fullScreenRequest = true;
    isFullscreenNow = document.webkitFullscreenElement !== null;
});
$(window).on('orientationchange', function (event) {
    fullScreenRequest = true;
    isFullscreenNow = document.webkitFullscreenElement !== null;
});

$(window).on('resize', function () {
    var _self = $(this);
    var resizeTimer;
    clearTimeout(resizeTimer);
    resizeTimer = setTimeout(function () {
        if (_self.width() !== width) { // Done resize only width ...
            width = _self.width();
            global.responsiveElementDesktop();
            footer.responsiveElement();
            lightbox.calcLightboxContent(lightBoxTypes.video);
            // showLessMore.setHeightItemList();
            navCate.initNavCateSwiper();
            if (!fullScreenRequest) {
                eventDetailPage.responsiveElement();
                detailPage.responsiveElement();
            }
            if (fullScreenRequest && !isFullscreenNow) {
                fullScreenRequest = false;
            }
            global.removeReadmoreButton();
            postLeftImg.breakTextLineSearch();
            detailPageTitle.reponsiveElement();
            global.scrollAdvertise();
        }
        navCate.initNavCateSwiper();
    }, 250);

});
$(window).scroll(function () {
    global.showElement();
    global.scrollToTop();
});

var sentIdAdvertise = [];

$(document.body).on('appear', '.advertising-fixed', function (e, $affected) {
    console.log(100);
});

$(document.body).on('appear', '.advertising-section .swiper-container, .advertising .swiper-container', function (e, $affected) {
    $(this).addClass('appear');
    // console.log($(this).find('.swiper-slide-active'));
    var requestModel = {
        ad_id: $(this).find('.swiper-slide-active').attr('ad-id') ? $(this).find('.swiper-slide-active').attr('ad-id').slice(1, -1) : null,
        path: window.location.pathname,
        page_id: $('#page_id').val() ? $('#page_id').val().slice(1, -1) : null
    };
    // console.log(requestModel);
    var sectionId = $(this).find('.swiper-slide-active').closest('.swiper-container').attr('section-id');
    var advertiseRootId = requestModel.ad_id ? sectionId.concat(requestModel.ad_id) : null;
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
});
$(document.body).on('disappear', '.advertising-section .swiper-container, .advertising .swiper-container', function (e, $affected) {
    $(this).removeClass('appear');
});
$('.advertising-section .swiper-container').appear();
$('.advertising .swiper-container').appear();

function Typer(callback) {
    eventDetailPage.responsiveElement();
    return true;
}