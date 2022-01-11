var detailPageTitle = {
    showSocialIconList: function () {
        $('.news-detail-share-img').on('click', function () {
            $(this).find('.column-social-block').toggleClass('active');
        });
    },
    reponsiveElement: function () {
        var columnSocialBlock = $('.column-social-block');
        if ($(window).width() > 1025) {
            $('.detail-image-block').after(columnSocialBlock);
        } else {
            $('.news-detail-share-img').append(columnSocialBlock);
        }
    },
    likeButton: function () {
        $('.detail-section .reaction-heart').on('click', function () {
            var _self = $(this);
            var currentHeartNumber = parseFloat(_self.find('.heart-number').text());
            var itemId = $('#CurrentItemId').val();
            var url = !_self.hasClass('active') ? '/ajax/like/add' : '/ajax/like/remove';
            var newHeartNumber = !_self.hasClass('active') ? currentHeartNumber + 1 : currentHeartNumber - 1;
            newHeartNumber = newHeartNumber < 0 ? 0 : newHeartNumber;
            $.ajax({
                url: url + '?item_id=' + itemId.toString(),
                success: function (result) {
                    if (result.IsSuccess) {
                        _self.toggleClass('active');
                        _self.find('.heart-number').text(newHeartNumber.toString());
                    }
                }
            });
        })
    },
}
