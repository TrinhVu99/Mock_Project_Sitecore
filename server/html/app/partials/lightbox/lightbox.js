var target = null;
var lightBoxTypes = {
	video: 'video',
	podcast: 'podcast',
	youtubeVideo: 'youtubeVideo'
}
var lightBoxClasses = {
	video: '.lightbox-video',
	podcast: '.lightbox-podcast',
	youtubeVideo: '.lightbox-youtube-video'
}
var lightbox = {
	initNiceScroll: function () {
		if ($('.niceScroll').length) {
			$('.niceScroll').niceScroll({
				cursorcolor: '#ccc',
				cursorwidth: '7px',
				autohidemode: false
			});
		}
	},
	getLightBoxTarget: function (lightBoxType) {
		switch (lightBoxType) {
		case lightBoxTypes.video:
			target = lightBoxClasses.video;
			break;
		default:
			target = lightBoxClasses.youtubeVideo;
		}
	},
	calcLightboxContent: function (lightBoxType) {
		lightbox.getLightBoxTarget(lightBoxType);
		var lightboxContainer = $(target).find('.lightbox');
		var lightboxIframe = lightboxContainer.find('.iframe-responsive-wrapper');
		var lightboxContent = lightboxContainer.find('.lightbox-content');
		var lightboxContentH = lightboxContainer.outerHeight() - lightboxIframe.outerHeight();
		lightboxContent.outerHeight(lightboxContentH);
	},
	openLightbox: function (lightBoxType, youtubeUrl, ajaxUrl) {
		if (!lightBoxType) return;
		lightbox.getLightBoxTarget(lightBoxType);
		$('.lightbox-overlay').fadeIn(timing.easingTime);
		global.overflowBody();
		$(target).find('.lightbox-content').html('');
		if (!youtubeUrl) {
			console.warn('Error! Must include video url, Video will play alternative source for development view');
			youtubeUrl = 'https://www.youtube.com/embed/4Hwh-4lJmq8'; // hardcode for development
		};
		if (!youtubeUrl.indexOf('?autoplay=1') >= 0) youtubeUrl += '?autoplay=1';
		var iframeHtml = '<iframe class="iframe-responsive" src="' + youtubeUrl + '" width="1440" height="549" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen=""></iframe>';
		$(target).find('.iframe-responsive-wrapper').append(iframeHtml);
		lightbox.showLightBoxLoader();
		$(target).fadeIn(timing.easingTime);
		if (ajaxUrl && ajaxUrl !== '') {
			$.ajax({
				url: ajaxUrl,
				success: function (result) {
					if (result.IsSuccess) {
						$(target).find('.lightbox-content').append(result.Html);
						if (lightBoxType === lightBoxTypes.video) {
							setTimeout(function () {
								lightbox.layoutLightboxContent('video'); // call after lightbox generated content
							}, 600);
						}
					}
				}
			});
		}
	},
	closeLightbox: function () {
		$('body').on('click', '.lightbox-wrapper .close, .lightbox-overlay', function () {
			global.clearOverflowBody();
			var iframe = $('.lightbox:visible iframe');
			$('.lightbox-wrapper:visible, .lightbox-overlay').fadeOut(timing.easingTime, function () {
				iframe.remove();
			});
		});
	},
	/* ctaOpenLightBox: function () {
		$('.video-section').on('click', '.card-img-left', function () {
			var videoSourceId = $('#VideoDetailDatasourceId').val();
			var videoId = $(this).closest('.video').attr('video-id');
			var videoUrl = $(this).closest('.video').attr('video-url');
			var videoDetailUrl = '/ajax/video/GetVideoDetail';
			var ajaxUrl = videoDetailUrl + '?videoId=' + videoId + '&datasourceId=' + videoSourceId;
			lightbox.openLightbox('video', videoUrl, ajaxUrl);
		});
	}, */
	showLightBoxLoader: function () {
		$('.lightbox .loader').show();
	},
	hideLightBoxLoader: function () {
		$('.lightbox .loader').hide();
	},
	layoutLightboxContent: function (lightBoxType) {
		// lightbox.hideLightBoxLoader();
		lightbox.calcLightboxContent(lightBoxType);
		lightbox.initNiceScroll();
	},
	openYoutubeVideoLightbox: function () {}
}
