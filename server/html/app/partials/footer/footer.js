// footer scripts
$.validator.addMethod('regex', function (value, element, regexp) {
    return this.optional(element) || regexp.test(value.toString().trim());
}, function (param, element) {
    return $(element).data('msg-regex');
});
var footer = {
    responsiveElement: function () {
        if ($(window).width() < 768) {
            $('.footer-menu-section').prepend($('.social-icon-block-footer'));
        } else {
            $('.footer-logo').after($('.social-icon-block-footer'));
        }
        // $('.social-icon-block').css('margin-bottom', $('footer').outerHeight());
    },
    expandMenu: function () {
        $('.footer-menu-list-title').on('click', function () {
            if ($(window).width() < 768) {
                $(this).toggleClass('active');
            }
        })
    },
    subscribeForm: function () {
        if ($('#e-newsletter-form').length) {
            $('#e-newsletter-form .email-input').on('input propertychange', function () {
                if ($(this).val()) {
                    $('.email-submit-button').addClass('active');
                } else {
                    $('.email-submit-button').removeClass('active');
                }
            });
        }
        $('#e-newsletter-form').validate({
            success: 'valid',
            onkeyup: function (ele) {
                var email = $('.email-input').val();
                if (email === '') {
                    $(ele).closest('.e-newsletter-input-section').find('#newsletter__register-error').hide();
                }
            },
            submitHandler: function (ele) {
                var email = $('.email-input').val();
                $.ajax({
                    url: '/ajax/emailsubscription/subscribe?email=' + email,
                    success: function (result) {
                        if (result.IsSuccess) {
                            $(ele).find('.e-newsletter-input-section').hide();
                            $(ele).find('.thanks-message').show();
                            $('.email-submit-button').addClass('disable');
                            $('.submit-email-text').addClass('disable');
                        } else if (result.StatusCode === 'EmailRegistered') {
                            var message = $('#SubscriptionEmailRegistered').val();
                            $(ele).find('#newsletter__register-error').html(message);
                            $(ele).find('#newsletter__register-error').show();
                        }
                    }
                });
            },
            rules: {
                newsletter__register: {
                    regex: /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/
                }
            },
            messages: {
                newsletter__register: {
                    email: $('#newsletter__register').data('msg-regex')
                    // required: $('#newsletter__register').data('msg-regex')
                }
            }
        });
    }
}
