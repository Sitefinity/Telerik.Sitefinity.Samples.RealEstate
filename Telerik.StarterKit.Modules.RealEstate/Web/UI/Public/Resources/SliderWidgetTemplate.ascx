<%@ Control Language="C#" %>



<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>

<sf:ResourceLinks id="resourceLinks" runat="server">
 <sf:ResourceFile JavaScriptLibrary="JQuery" />
 <sf:ResourceFile Name="Telerik.StarterKit.Modules.RealEstate.Resources.Scripts.jquery.easing.1.3.js" AssemblyInfo="Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.SliderWidget, Telerik.StarterKit.Modules.RealEstate"  />
 <sf:ResourceFile Name="Telerik.StarterKit.Modules.RealEstate.Resources.Scripts.jquery.jcarousel.min.js" AssemblyInfo="Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.SliderWidget, Telerik.StarterKit.Modules.RealEstate"  />
</sf:ResourceLinks>

<telerik:RadListView ID="SlidesList" runat="server" ItemPlaceholderID="ItemsContainer" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
    <LayoutTemplate>
		<div class="properties-slides">
            <asp:PlaceHolder ID="ItemsContainer" runat="server" />
		</div><!-- .properties-slides -->
    </LayoutTemplate>
    <ItemTemplate>
	    <div class="item">
		    <asp:Image ID="imgSlider" runat="server" />
		    <div class="information-area">
			    <div class="information-area-left">
				    <h2><asp:Literal ID="ltrAddress" runat="server" /></h2>
				    <div class="price"><asp:Literal ID="ltrPrice" runat="server" /></div>
				    <asp:HyperLink ID="hlDetails" runat="server" Text="<%$Resources:RealEstateResources, SliderSeeDetails %>" />
			    </div>
			    <div class="information-area-right">
                    <telerik:RadListView ID="FeaturesList" runat="server" ItemPlaceholderID="FeaturesContainer" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
                        <LayoutTemplate>
				            <ul>
                                <asp:PlaceHolder ID="FeaturesContainer" runat="server" />
		                    </ul>
                        </LayoutTemplate>
                        <ItemTemplate>
	                        <li><asp:Literal ID="ltrFeature" runat="server" /></li>
                        </ItemTemplate>
                    </telerik:RadListView>
			    </div>
		    </div><!-- .information-area -->
	    </div><!-- .item -->
    </ItemTemplate>
</telerik:RadListView>

<telerik:RadListView ID="CarouselList" runat="server" ItemPlaceholderID="ItemsContainer" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
    <LayoutTemplate>
		<div class="properties-carousel">
            <ul>
                <asp:PlaceHolder ID="ItemsContainer" runat="server" />
            </ul>
		</div><!-- .properties-carousel -->
    </LayoutTemplate>
    <ItemTemplate>
	    <li>
		    <a href="#"><asp:Image ID="imgThumbnail" runat="server" /></a>
            <div>
				<h3><asp:Literal ID="ltrAddress" runat="server" /></h3>
				<p><asp:Literal ID="ltrPrice" runat="server" /></p>
				<asp:HyperLink ID="hlDetails" runat="server" Text="<%$Resources:RealEstateResources, CarouselSeeDetails %>" />
			</div>
	    </li>
    </ItemTemplate>
</telerik:RadListView>
<script language="javascript" type="text/javascript">
$(document).ready(function(){
    	// globals
	var speed = 500,
		speedFast = Math.round(speed/2);
	
	/*---------------------------------------------------------------------------------- CAROUSELS */
	var slides = $('.properties-slides');
	$('.properties-carousel').jcarousel({
		scroll:1,
		duration:speed,
		easing:'easeOutQuart',
		initCallback:function(){
			$('.properties-carousel ul').css({margin:'-30px 0 0',padding:'15px 0 0'})
			.find('li').wrapInner('<div class="inner" />').eq(0).addClass('current');
		}
	});

    slides.children(':visible').hide();
    slides.children(':nth-child(1)').show();

	$('.properties-carousel li a').click(function(e){
		var elem = $(this),
			li = elem.parents('li'),
			index = li.index()+1;
			
		li.addClass('current').siblings().removeClass('current');
		slides.children(':visible').fadeOut(speed);
		slides.children(':nth-child('+index+')').fadeIn(speed);
		e.preventDefault();
	});
	
	$('.more-properties .carousel').jcarousel({
		scroll:1,
		duration:speedFast
	});
	
	var propertyCarousel = $('.property-carousel');
	propertyCarousel.jcarousel({
		scroll:1,
		duration:speedFast,
		initCallback:function(c){
			var carousel = c.container;
			carousel.append('<div class="info"><span class="left" /><span class="right" /></div>');
			
			var info = carousel.find('.info'),
				title = info.find('.left'),
				counter = title.next(),
				prev = carousel.find('.jcarousel-prev'),
				next = prev.next(),
				imgs = carousel.find('img'),
				img = imgs.filter(':first'),
				pattern = /([0-9]+)\/[0-9]+/;
				
			title.text(img[0].alt);
			counter.text('1/'+imgs.length);
			
			var thumbs = carousel.parent().find('.property-carousel-thumbs');
			if(thumbs.length){
				var thumbLinks = thumbs.find('a');
				thumbLinks.click(function(e){
					var elem = $(this),
						href = elem.attr('href'),
						index = thumbLinks.index(this)+1;
					
					img = carousel.find('li:nth-child('+index+') img');
					updateCarouselBottomText(img[0].alt,index);
					elem.addClass('current').parent().siblings().find('a').removeClass('current');
					c.scroll($.jcarousel.intval(index));
					//console.log(index);
					e.preventDefault();
				}).filter(':first').addClass('current');
			}
			
			next.click(function(e){
				img = img.parents('li').next().find('img');
				if(img.length){
					var num = parseInt(counter.text().match(pattern));
					updateCarouselBottomText(img[0].alt,++num);
					if(thumbs.length){
						thumbLinks.eq(num-1).addClass('current').parent().siblings().find('a').removeClass('current');
					}
				} else img = imgs.filter(':last');
			});
			
			prev.click(function(e){
				img = img.parents('li').prev().find('img');
				if(img.length){
					var num = parseInt(counter.text().match(pattern));
					updateCarouselBottomText(img[0].alt,--num);
					if(thumbs.length){
						thumbLinks.eq(num-1).addClass('current').parent().siblings().find('a').removeClass('current');
					}
				} else img = imgs.filter(':first');
			});
			
			function updateCarouselBottomText(text,num){
				title.text(text);
				counter.text(num+'/'+imgs.length);
			}
		}
	});
	
	$('.property-carousel-thumbs').jcarousel({
		scroll:5,
		duration:speedFast,
		initCallback:function(e){
			
		}
	});
});
</script>