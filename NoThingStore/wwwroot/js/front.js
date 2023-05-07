'use strict';

document.addEventListener('DOMContentLoaded', function () {
	const lightbox = GLightbox({
		touchNavigation: true,
	});

	var productSliderThumbs = new Swiper('.product-slider-thumbs', {
		direction: 'horizontal',
		slidesPerView: 5,
		freeMode: true,
		spaceBetween: 1,
	});

	var productsSlider = new Swiper('.product-slider', {
		slidesPerView: 1,
		spaceBetween: 0,
		loop: true,
		thumbs: {
			swiper: productSliderThumbs,
		},
	});
});