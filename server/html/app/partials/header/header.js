var el = {
    mainNav: $('.main-nav')
}
var globalSearchSubmited = false;
var header = {
    openMainNav: function () {
        $('body').on('click', '.menu-icon', function () {
            $('.main-nav').slideDown(timing.easingTime, function () {
                global.overflowBody();
            });
        });
    },
    closeMainNav: function () {
        $('.main-nav').on('click', '.close', function () {
            $('.main-nav').slideUp(timing.easingTime, function () {
                global.clearOverflowBody();
            });
        });
    },
    toggleSearchGlobal: function () {
        $('body').on('click', '.search-global-icon', function () {
            $('.search-global').stop().slideToggle();
            $('.search-global-error-message').removeClass('active');
            $('.search-global input').val('');
            $('.search-global input').focus();
        });
    },
    initSearchGlobalValidation: function () {
        $('.search-global form').validate();
    },
    getLocation: function () {
        $('.degree').text('');
        var now = new Date();
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(
                function (position) {
                    var location = {
                        latlng: {
                            lat: position.coords.latitude,
                            lng: position.coords.longitude
                        }
                    }
                    header.getDegree(location.latlng);
                },
                function (error) {
                    if (error.code === 1) {
                        var latlng = {
                            lat: 13.736717,
                            lng: 100.523186
                        }
                        header.getDegree(latlng); // bangkok
                    } else {
                        console.log(error.message);
                    }
                });
        } else {
            console.log('Geolocation is not supported by browser');
        }
    },
    getDegreeApiUrl: function (location) {
        var latestEventApiUrl = '/ajax/Temperature/GetCurrentTemperature';
        var url = latestEventApiUrl + '?latitude=' + location.lat + '&longitude=' + location.lng;
        return url
    },
    toggleLang: function () {
        $('body').on('click', '.language', function () {
            $(this).toggleClass('active-en');
        });
    },
    getDegree: function (latlng) {
        $.ajax({
            url: header.getDegreeApiUrl(latlng),
            success: function (result) {
                if (result.IsSuccess && result.AirTemperature) {
                    $('.degree').text(result.AirTemperature);
                }
            }
        });
    },
    getAutocompleteTermUrl: function (term, device_type, datasource_id) {
        var latestEventApiUrl = '/ajax/search/suggestedkeywords';
        var url = latestEventApiUrl + '?term=' + term + '&device_type=' + device_type + '&datasource_id=' + datasource_id;
        return url
    },
    getAutocompleteTerm: function () {
        $('.search-input').autocomplete({
            maxHeight: 200,
            lookup: function (query, done) {
                // Do Ajax call or lookup locally, when done,
                // call the callback and pass your results:
                var term = $('.search-global input').val();
                var bodyWidth;
                var datasourceId = $('#SearchBarDatasourceId').val();
                if ($(window).width() < 768) {
                    bodyWidth = 'mobile'
                } else if ($(window).width() < 1025) {
                    bodyWidth = 'tablet'
                };
                if (term != '') {
                    $('.search-global-error-message').removeClass('active');
                };
                $.ajax({
                    url: header.getAutocompleteTermUrl(term, bodyWidth, datasourceId),
                    success: function (result) {
                        if (result.IsSuccess && result.Data) {
                            var listTerm = [];
                            // var listTerm = ['abc', 'abc'];
                            $.each(result.Data, function (i, obj) {
                                var value = {
                                    value: obj
                                }
                                listTerm.push(value);
                            });
                            var result = {
                                suggestions: listTerm
                            };
                            done(result);
                        }
                    }
                });
                // var listTerm = [];
                // var result = ['asdasd', 'asdasd', 'asdasd', 'asdasd', 'asdasd', 'asdasd', 'asdasd', 'asdasd', 'asdasd', 'asdasd', 'asdasd'];
                // $.each(result, function (i, obj) {
                //     var value = {
                //         value: obj
                //     }
                //     listTerm.push(value);
                // });
                // var result = {
                //     suggestions: listTerm
                // };
                // done(result);
            },
            onSelect: function (suggestion) {
                window.location.href = '/search?term=' + encodeURIComponent(suggestion.value);
            },
            onHide: function (container) {
                var term = $('.search-global input').val();
                // console.log(globalSearchSubmited);
                if (globalSearchSubmited && term == '') {
                    $('.search-global-error-message').addClass('active');
                }
            },
            triggerSelectOnValidInput: false
        });
    },
    searchPage: function () {
        $('.search-global .header-search-icon').on('click', function () {
            header.validateSearchInput();
        });
        $('.search-input').keyup(function (e) {
            if (e.keyCode == 13) {
                header.validateSearchInput();
            }
        });
    },
    validateSearchInput: function () {
        var term = encodeURIComponent($('.search-global input').val());
        globalSearchSubmited = true;
        if (term != '') {
            if ($('.search-global input').val().length < 260) {
                window.location.href = '/search?term=' + term;
            } else {
                $('.search-global-error-message#max-length-message').addClass('active');
            }
        } else {
            $('.search-global-error-message').addClass('active');
        }
    },
    showSearchBar: function () {
        var searchPage = $('.search-results-section');
        // console.log(searchPage);
        if (searchPage.length) {
            $('.search-global').css('display', 'block');
        }

    },
    autocompleteTermScroll: function () {
        $('.autocomplete-suggestions').niceScroll({
            horizrailenabled: false,
            cursorcolor: '#E6E6E6',
            cursorwidth: '7px',
            // railpadding: { top: 10, right: 3, left: 0, bottom: 10 }
        });
        // $('.autocomplete-suggestions').css('over');
    },
    showPopUpMenuLink: function () {
        $('.nav-cate-item').each(function () {
            var item = $(this).html();
            var active = $(this).hasClass('active') ? 'active' : '';
            var html = '<li class="' + active + '">' + item + '</li>'
            $('.nav-mobile ul').append(html);
        })
    },
}
