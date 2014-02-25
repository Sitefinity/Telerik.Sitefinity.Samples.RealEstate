

document.documentElement.className += ' has-js ';

$(document).ready(function () {
    	
    	// globals
    	var speed = 500,
    		speedFast = Math.round(speed/2);
    	
    //	/*---------------------------------------------------------------------------------- CAROUSELS */
    //	var slides = $('.properties-slides');
    //	$('.properties-carousel').jcarousel({
    //		scroll:1,
    //		duration:speed,
    //		easing:'easeOutQuart',
    //		initCallback:function(){
    //			$('.properties-carousel ul').css({margin:'-30px 0 0',padding:'15px 0 0'})
    //			.find('li').wrapInner('<div class="inner" />').eq(0).addClass('current');
    //		}
    //	});
    //	
    //	$('.properties-carousel li a').click(function(e){
    //		var elem = $(this),
    //			li = elem.parents('li'),
    //			index = li.index()+1;
    //			
    //		li.addClass('current').siblings().removeClass('current');
    //		slides.children(':visible').fadeOut(speed);
    //		slides.children(':nth-child('+index+')').fadeIn(speed);
    //		e.preventDefault();
    //	});
    //	
    //	$('.more-properties .carousel').jcarousel({
    //		scroll:1,
    //		duration:speedFast
    //	});
    //	
    //	var propertyCarousel = $('.property-carousel');
    //	propertyCarousel.jcarousel({
    //		scroll:1,
    //		duration:speedFast,
    //		initCallback:function(c){
    //			var carousel = c.container;
    //			carousel.append('<div class="info"><span class="left" /><span class="right" /></div>');
    //			
    //			var info = carousel.find('.info'),
    //				title = info.find('.left'),
    //				counter = title.next(),
    //				prev = carousel.find('.jcarousel-prev'),
    //				next = prev.next(),
    //				imgs = carousel.find('img'),
    //				img = imgs.filter(':first'),
    //				pattern = /([0-9]+)\/[0-9]+/;
    //				
    //			title.text(img[0].alt);
    //			counter.text('1/'+imgs.length);
    //			
    //			var thumbs = carousel.parent().find('.property-carousel-thumbs');
    //			if(thumbs.length){
    //				var thumbLinks = thumbs.find('a');
    //				thumbLinks.click(function(e){
    //					var elem = $(this),
    //						href = elem.attr('href'),
    //						index = thumbLinks.index(this)+1;
    //					
    //					img = carousel.find('li:nth-child('+index+') img');
    //					updateCarouselBottomText(img[0].alt,index);
    //					elem.addClass('current').parent().siblings().find('a').removeClass('current');
    //					c.scroll($.jcarousel.intval(index));
    //					//console.log(index);
    //					e.preventDefault();
    //				}).filter(':first').addClass('current');
    //			}
    //			
    //			next.click(function(e){
    //				img = img.parents('li').next().find('img');
    //				if(img.length){
    //					var num = parseInt(counter.text().match(pattern));
    //					updateCarouselBottomText(img[0].alt,++num);
    //					if(thumbs.length){
    //						thumbLinks.eq(num-1).addClass('current').parent().siblings().find('a').removeClass('current');
    //					}
    //				} else img = imgs.filter(':last');
    //			});
    //			
    //			prev.click(function(e){
    //				img = img.parents('li').prev().find('img');
    //				if(img.length){
    //					var num = parseInt(counter.text().match(pattern));
    //					updateCarouselBottomText(img[0].alt,--num);
    //					if(thumbs.length){
    //						thumbLinks.eq(num-1).addClass('current').parent().siblings().find('a').removeClass('current');
    //					}
    //				} else img = imgs.filter(':first');
    //			});
    //			
    //			function updateCarouselBottomText(text,num){
    //				title.text(text);
    //				counter.text(num+'/'+imgs.length);
    //			}
    //		}
    //	});
    //	
    //	$('.property-carousel-thumbs').jcarousel({
    //		scroll:5,
    //		duration:speedFast,
    //		initCallback:function(e){
    //			
    //		}
    //	});
    //	
    //	/*--------------------------------------------------------------------------------------- TABS */
    //	//tabs( $('.property-item ul.tabs li:not(.contact-agent)') , true );
    //	tabs( $('.search-advanced .tabs li') , false );
    //	function tabs(tabs,urlfriendly){ // @param tabs: jquery obj, @param urlfriendly: boolean
    //		var hash = /(#.+)$/,
    //			//tabs = $('.property-item ul.tabs li:not(.contact-agent)'),
    //			urlfriendly = urlfriendly || false,
    //			tabDivs = '',
    //			tabView = window.location.href;//.toString().match(hash)[1];
    //		
    //		if(tabs.length){
    //			if(urlfriendly){
    //				tabView = tabView.match(hash);
    //				if(typeof(tabView)=='undefined' || !tabView){
    //					tabView = tabs.filter(':first').find('a')[0].href.match(hash)[1];
    //				} else {
    //					tabView = tabView[1];
    //				}
    //			} else {
    //				tabView = tabs.filter(':first').find('a')[0].href.match(hash)[1];
    //			}
    //			
    //			tabs.find('a').click(function(e){
    //				var elem = $(this),
    //					href = this.href.match(hash)[1],
    //					currentView = tabDivs.filter(href);
    //				
    //				currentView.attr('id','fake-id').show();
    //				tabDivs.not(currentView).hide();
    //				elem.parent().addClass('current').siblings().removeClass('current');
    //				setTimeout(function(){
    //					currentView.attr('id',href.substr(1));
    //				},20);
    //				
    //				if(!urlfriendly) e.preventDefault();
    //				
    //			}).each(function(){ // get all tab items
    //				tabDivs += (tabDivs=='' ? '' : ',') + $(this).attr('href');
    //			});
    //			
    //			tabDivs = $(tabDivs);
    //			tabs.filter(':has(a[href*="'+tabView+'"])').addClass('current');
    //			tabDivs.filter(':not('+tabView+')').hide();
    //			
    //		}
    //	}
    //	
    //	/*---------------------------------------------------------------------------- ADVANCED SEARCH */
    //	var searchBar = $('.search-bar'),
    //		searchAdv = $('.search-advanced'),
    //		searchExpand = searchBar.find('.round-btn-dark');
    //		
    //	searchExpand.data({show:'Advanced search',hide:'Close search'}).click(function(e){
    //		var elem = $(this);
    //		searchAdv.toggle();
    //		if(searchAdv.filter(':visible').length){
    //			elem.find('span>span>span').text(elem.data('hide'));
    //			elem.removeClass('collapsed').addClass('expanded');
    //		} else {
    //			elem.find('span>span>span').text(elem.data('show'));
    //			elem.addClass('collapsed').removeClass('expanded');
    //		}
    //		e.preventDefault();
    //	});
    //	
    //	/*--------------------------------------------------------------------------------- LIGHTBOXES */
    //	
    //	$('.property-carousel li a').fancybox({
    //		padding:10,
    //		overlayColor:'#000',
    //		overlayOpacity:0.7,
    //		speedIn:speedFast,
    //		speedOut:speedFast,
    //		changeSpeed:speedFast,
    //		centerOnScroll:true,
    //		titlePosition:'inside',
    //		onComplete:function(a,b,c){ // a: all group links (jQuery); b: index; c: fancybox object
    //			$('#fancybox-title-inside').prepend('<span class="right">'+(b+1)+' / '+a.length+'</span>');
    //			$('#fancybox-content').prepend($('.property-item .row .column').find('h1,address').clone());
    //		}
    //	});

});

