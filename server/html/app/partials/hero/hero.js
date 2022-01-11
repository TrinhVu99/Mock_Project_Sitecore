var hero = {
	openVideoLightbox: function () {
		$('.hero-banner').on('click', '.video-item', function () {
			var videoUrl = $(this).attr('video-url');
			lightbox.openLightbox('youtubeVideo', videoUrl);
		});
	}
}
