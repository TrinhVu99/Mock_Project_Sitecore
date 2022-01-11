var detailPage = {
    responsiveElement: function () {
        var newsDetailTrendingPostCol = $('.news-detail .trending-post-col');
        var newsDetailImageBlock = $('.news-detail .detail-image-block');
        var newsDetailAuthorSection = $('.news-detail .author-section-detail-page');
        var advertisingTrendingPost = $('.news-detail .advertise-trending-post');
        var newsDetailAuthorDescription = $('.news-detail .author-section-description');
        var newsDetailTagsSection = $('.tags-section-detail-page');
        if ($(window).width() < 768) {
            $('.news-detail .trending-post-row').prepend(newsDetailTrendingPostCol);
            $('.news-detail .news-detail-title-block').after(newsDetailImageBlock);
            $('.news-detail .author-section-detail-page-col').prepend(newsDetailImageBlock);
            newsDetailTrendingPostCol.append(advertisingTrendingPost);
            // newsDetailImageBlock.append(newsDetailAuthorDescription);
            $('news-detail .detail-maincontent-container').after(newsDetailTagsSection);
        } else if ($(window).width() > 1025) {
            $('.news-detail .detail-maincontent-row').append(newsDetailTrendingPostCol);
            $('.news-detail .detail-maincontent-col').prepend(newsDetailImageBlock);
            $('.news-detail .detail-maincontent-text').after(newsDetailAuthorSection);
            $('.news-detail .detail-maincontent-text').after(newsDetailTagsSection);
            newsDetailTrendingPostCol.prepend(advertisingTrendingPost);
            // newsDetailImageBlock.append(newsDetailAuthorDescription);
        } else {
            $('.news-detail .detail-maincontent-row').append(newsDetailTrendingPostCol);
            $('.news-detail .news-detail-title-block').after(newsDetailImageBlock);
            $('.news-detail .author-section-detail-page-col').prepend(newsDetailAuthorSection);
            newsDetailTrendingPostCol.append(advertisingTrendingPost);
            $('.news-detail .author-section-name-text').append(newsDetailAuthorDescription);
            $('news-detail .detail-maincontent-container').after(newsDetailTagsSection);
        }
    },
    moveAdvertisingSection: function () {
        $('.news-detail-readmore-btn button').on('click', function () {
            $('.advertise-readmore-md').addClass('fadeOut');
            setTimeout(function () {
                $('.readmore-button-advertise').hide();
            }, 500);
            if ($(window).width() < 1025) {
                $('.detail-maincontent-advertising-section').append($('.adverstising-section-unread .advertising-section'));
            }
        })
    }
}
