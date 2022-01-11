var eventDetailPage = {
    responsiveElement: function () {
        var eventInfoSection = $('.event-info-section');
        var eventDetailTrendingPostCol = $('.event-detail .trending-post-col');
        var eventDetailAdvertiseTrendingPost = $('.event-detail .advertise-trending-post');
        var detailImageBlock = $('.detail-image-block');
        if ($(window).width() < 768) {
            $('.event-detail .detail-image-block').after(eventInfoSection);
            $('.event-detail .trending-post-row').prepend(eventDetailTrendingPostCol);
            eventDetailTrendingPostCol.append(eventDetailAdvertiseTrendingPost);
            $('.news-detail-title-block').after(detailImageBlock);
            $('.author-section-detail-page').append($('.author-section-description'));
        } else if ($(window).width() > 1025) {
            $('.event-detail .detail-maincontent-block').prepend(eventInfoSection);
            $('.detail-maincontent-col').prepend(detailImageBlock);
            $('.event-detail .detail-maincontent-row').append(eventDetailTrendingPostCol);
            $('.event-detail .trending-post-col').prepend(eventDetailAdvertiseTrendingPost);
            $('.author-section-detail-page').append($('.author-section-description'));
        } else {
            $('.event-detail .detail-maincontent-block').prepend(eventInfoSection);
            $('.event-detail .detail-maincontent-row').append(eventDetailTrendingPostCol);
            eventDetailTrendingPostCol.prepend(eventDetailAdvertiseTrendingPost);
            $('.news-detail-title-block').after(detailImageBlock);
        }
        if ($('.detail-maincontent').hasClass('active') || $('.detail-maincontent-block').hasClass('active')) {
            $('.detail-readmore-btn button').addClass('fadeOut');
            $('.event-detail .trending-post-section').show();
        }
    },
    showUpcomingSidebar: function () {
        $('.event-detail .news-detail-readmore-btn button').on('click', function () {
            $('.advertise-readmore-md').addClass('fadeOut');
            $('.event-detail .trending-post-section').show();
        })
    }
}
