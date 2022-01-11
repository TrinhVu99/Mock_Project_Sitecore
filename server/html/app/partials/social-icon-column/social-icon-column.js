var socialIconColumn = {
    lineShareButton: function () {
        var $lineShareButton = $('.column-social-icon.is__Line-Share');
        if($lineShareButton.length) {
            $($lineShareButton).on('click', function (event) {
                event.preventDefault();
                var windowLineShare = window.open('https://social-plugins.line.me/lineit/share?url=' + window.location.href, 'popupWindow', 'width=600, height=600, scrollbars=yes');
            });
        }
    }
}