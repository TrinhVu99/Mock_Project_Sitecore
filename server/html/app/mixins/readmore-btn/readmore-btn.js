var readmoreButton = {
    continueReading: function () {
        $('.news-detail-readmore-btn button').on('click', function () {
            $('.fadedSection').addClass('active');
            global.removeMaxHeight($('.fadedSection'));
            $('.news-detail-readmore-btn').hide();
            $('.advertise-readmore-md').hide();
        })
    }
}
