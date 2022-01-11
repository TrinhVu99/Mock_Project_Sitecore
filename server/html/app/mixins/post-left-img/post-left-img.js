var postLeftImg = {
	breakTextLine: function () {
		global.breakTextLine($('.card-text'), 60);
	},
	breakTextLineSearch: function () {
		var _w = $(window).width();
		if (_w < 768) {
			global.breakTextLine($('.card-text-ext'), 95);
			global.breakTextLine($('.search-result-item .info p'), 120);
		} else if(_w < 1025) {
			global.breakTextLine($('.card-text-ext'), 30);
			global.breakTextLine($('.search-result-item .info p'), 100);
		} else {
			global.breakTextLine($('.card-text-ext'), 100);
			global.breakTextLine($('.search-result-item .info p'), 120);
		}
	}
}
