// header scripts
var bloggerComponent = {
    page_index: 1,
    showMoreBtn: function () {
        $('.bloggers-section .show-more').on('click', function () {
            // e.preventDefault();
            var self = $(this);
            var datasourceId = $('#BloggersRabbit_DatasourceId').val();
            var categoryId = $('#BloggersRabbit_CategoryId').val();
            var pageIndex = bloggerComponent.page_index;
            var url = '/ajax/article/getarticles' + '?datasource_id=' + datasourceId + '&category_id=' + categoryId + '&page_index=' + pageIndex;
            // console.log(bloggerComponent.page_index);
            $.ajax({
                url: url,
                success: function (result) {
                    if (result.IsSuccess) {
                        bloggerComponent.page_index++;
                        $('.bloggers-section .blogger-items').append(result.Html);
                        if ($('.bloggers-section .blogger-items .col-md-4').length >= $('#BloggersRabbit_TotalArticles').val()) {
                            self.hide();
                        };
                        global.replaceImgToBackground(self.closest('.bloggers-section').find('.img-to-bg img'));
                        self.closest('.bloggers-section').find('.expand-item-wrapper').css('height', 'auto');
                    }
                }
            });
        })
    },
}
