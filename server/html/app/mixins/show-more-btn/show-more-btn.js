var easingTime = 300;
var easingTimeSlow = 1200;
var pageIndexInitial = 2;
var pageIndex = null;
var itemList = [
    {
        mobileMinRow: 3,
        tabletMinRow: 1,
        wrapperClassName: '.blogger-items',
        itemClassName: '.blogger'
    },
    {
        mobileMinRow: 1,
        tabletMinRow: 1,
        wrapperClassName: '.upcoming-items-type',
        itemClassName: '.post'
    },
    {
        mobileMinRow: 2,
        tabletMinRow: 1,
        wrapperClassName: '.around-world-type',
        itemClassName: '.post'
    },
    {
        mobileMinRow: 1,
        tabletMinRow: 'auto',
        wrapperClassName: '.trending-item-group',
        itemClassName: '.post-left-img'
    }, {
        mobileMinRow: 3,
        tabletMinRow: 2,
        wrapperClassName: '.latest-event-type',
        itemClassName: '.post'
    }
]
var showLessMore = {
    setHeightItemList: function () {
        // for (var item of itemList) {
        for (var i = 0; i < itemList.length; i++) {
            var item = itemList[i];
            var wrapper = item.wrapperClassName;
            var bloggerSection = $(wrapper).closest('section').hasClass('bloggers-section');
            if (!bloggerSection) {
                if (!$(wrapper).hasClass('active')) {
                    var itemHeight = Math.floor($(wrapper).find(item.itemClassName).outerHeight()) - 2;
                    var mobileItemheight = 0;
                    var tabletItemheight = 0;
                    $(wrapper).find(item.itemClassName).each(function (id, el) {
                        if (id < item.mobileMinRow) {
                            mobileItemheight = mobileItemheight + Math.floor($(this).outerHeight());
                        }
                        if (item.tabletMinRow === 'auto') {
                            tabletItemheight = 0;
                        } else {
                            if (id < 3) {
                                tabletItemheight = tabletItemheight > Math.floor($(this).outerHeight()) ? tabletItemheight : Math.floor($(this).outerHeight());
                            }
                        }
                    })
                    if ($(window).width() < 768) {
                        if ($(wrapper).children().length < item.mobileMinRow) {
                            $(wrapper).height('auto');
                            $(wrapper).siblings('.more-btn').hide();
                        } else {
                            if (mobileItemheight > 0) {
                                $(wrapper).outerHeight(mobileItemheight);
                            } else {
                                $(wrapper).height('auto');
                            }
                        }
                    } else if ($(window).width() < 1025) {
                        if ($(wrapper).children().length <= (3 * item.tabletMinRow)) {
                            $(wrapper).height('auto');
                            $(wrapper).siblings('.more-btn').hide();
                        } else {
                            if (tabletItemheight > 0) {
                                $(wrapper).outerHeight(tabletItemheight * item.tabletMinRow);
                            } else {
                                $(wrapper).height('auto');
                            }
                        }
                    } else {
                        $(wrapper).height('auto');
                    }
                }
            }
        }
    },
    expandItem: function () {
        $('body').on('click', '.show-more', function (e) {
            e.preventDefault();
            var self = $(this);
            var bloggerSection = self.closest('section').hasClass('bloggers-section');
            // console.log(bloggerSection);
            var expandItemWrapper = $(this).parent().siblings('.expand-item-wrapper');
            var scrollH = expandItemWrapper && expandItemWrapper[0].scrollHeight;
            if (!bloggerSection) {
                if (expandItemWrapper.hasClass('active') || $(window).width() > 1024) {
                    showLessMore.getLatestEvent(pageIndex, function (tobeAppendHtml, totalItems) {
                        showLessMore.appendLatestEvent(tobeAppendHtml);
                        global.replaceImgToBackground($('.img-to-bg img'));
                        expandItemWrapper.addClass('active');
                        var shownItemsLength = self.parent().siblings('.lazy-load-type').children().length;
                        if (shownItemsLength >= totalItems) self.fadeOut();
                    });
                } else {
                    console.log(bloggerSection);
                    expandItemWrapper.outerHeight(scrollH);
                    expandItemWrapper.addClass('active');
                    !expandItemWrapper.hasClass('lazy-load-type') && $(this).removeClass('active').siblings('.show-less').addClass('active');
                    setTimeout(function () {
                        expandItemWrapper.height('auto');
                    }, 500);
                }
            }
        });
    },
    collapseItem: function () {
        $('body').on('click', '.show-less', function (e) {
            e.preventDefault();
            var expandItemWrapper = $(this).parent().siblings('.expand-item-wrapper');
            expandItemWrapper.removeClass('active');
            expandItemWrapper.outerHeight(expandItemWrapper.outerHeight());
            showLessMore.setHeightItemList();
            var offset = expandItemWrapper.closest('section').offset().top;
            global.scrollTo(offset, easingTimeSlow);
            $(this).removeClass('active').siblings('.show-more').addClass('active');
        });
    },
    appendLatestEvent: function (tobeAppendHtml) {
        $('.latest-event-type').append(tobeAppendHtml);
    },
    getLatestEvent: function (_pageIndex, callback) {
        pageIndex = _pageIndex || pageIndexInitial;
        var domain = window.location.origin;
        var dataSourceId = $('#LastestEventDatasourceId').val();
        var latestEventApiUrl = '/ajax/Event/GetLastestEvent';
        var url = latestEventApiUrl + '?pageIndex=' + pageIndex + '&datasourceId=' + dataSourceId;
        $.ajax({
            url: url,
            success: function (result) {
                if (result.IsSuccess) {
                    var tobeAppendHtml = result && result.Html ? result.Html : '';
                    var totalItems = result && result.TotalRecord ? result.TotalRecord : 0;
                    pageIndex++;
                    if (typeof callback === 'function') callback(tobeAppendHtml, totalItems);
                }
            }
        });
    }
}
