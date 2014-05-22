<%@ Control Language="C#" %>



<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI.ContentUI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<sf:ResourceLinks id="resourceLinks" runat="server">
 <sf:ResourceFile JavaScriptLibrary="JQuery" />
 <sf:ResourceFile JavaScriptLibrary="JQueryFancyBox"></sf:ResourceFile>
 <sf:ResourceFile Name="Telerik.StarterKit.Modules.RealEstate.Resources.Scripts.jquery.jcarousel.min.js" AssemblyInfo="Telerik.StarterKit.Modules.RealEstate.Web.UI.Public.DetailsView, Telerik.StarterKit.Modules.RealEstate"  />
</sf:ResourceLinks>

<div class="property-item">
    <telerik:RadListView ID="DetailsView" ItemPlaceholderID="ItemsContainer" runat="server" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
        <LayoutTemplate>
            <asp:PlaceHolder ID="ItemsContainer" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
            <div class="row self-clear">
                <div class="exampleWrapper">
                    <telerik:RadTabStrip id="tabStrip" MultiPageID="multiPage" runat="server" SelectedIndex="0" EnableEmbeddedSkins="false" OnClientTabSelected="tabSelectedHandler">
                        <Tabs>
                            <telerik:RadTab Text="<%$Resources:RealEstateResources, Overview %>" PageViewID="pageOverview"/>
                            <telerik:RadTab Text="<%$Resources:RealEstateResources, Photos %>" PageViewID="pagePhotos" />
                            <telerik:RadTab Text="<%$Resources:RealEstateResources, PanaromicView %>" PageViewID="pagePanaromicView" />
                            <telerik:RadTab Text="<%$Resources:RealEstateResources, FloorPlan %>" PageViewID="pageFloorPlan" />
                            <telerik:RadTab Text="<%$Resources:RealEstateResources, MapAndLocalArea %>" PageViewID="pageMap" />
                            <telerik:RadTab Text="<%$Resources:RealEstateResources, ContactOurAgent %>" NavigateUrl="~/" CssClass="contactAgent" />
                        </Tabs>
                    </telerik:RadTabStrip>

                    <telerik:RadMultiPage id="multiPage" SelectedIndex="0" runat="server" CssClass="pageView">
                        <telerik:RadPageView id="pageOverview" runat="server">
                        

                            <telerik:RadListView ID="rlvOverview" runat="server" ItemPlaceholderID="Container" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
                                <LayoutTemplate>
                                    <div class="property-carousel">
				                        <ul>
                                            <asp:PlaceHolder ID="Container" runat="server" />
		                                </ul>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
	                                <li><asp:Image ID="imgPhoto" runat="server" ImageUrl='<%# Eval("Url") %>' AlternateText='<%# Eval("Title") %>' /></li>
                                </ItemTemplate>
                            </telerik:RadListView>


                        </telerik:RadPageView>
                        <telerik:RadPageView id="pagePhotos" runat="server">
                        
                            <telerik:RadListView ID="rlvPhotos" runat="server" ItemPlaceholderID="Container" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
                                <LayoutTemplate>
                                    <div class="property-carousel">
				                        <ul>
                                            <asp:PlaceHolder ID="Container" runat="server" />
		                                </ul>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
	                                <li><asp:Image ID="imgPhoto" runat="server" ImageUrl='<%# Eval("Url") %>' AlternateText='<%# Eval("Title") %>' /></li>
                                </ItemTemplate>
                            </telerik:RadListView>

                        </telerik:RadPageView>
                        <telerik:RadPageView id="pagePanaromicView" runat="server">
                        
                            <telerik:RadListView ID="rlvPanaromicView" runat="server" ItemPlaceholderID="Container" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
                                <LayoutTemplate>
                                    <div class="property-carousel">
				                        <ul>
                                            <asp:PlaceHolder ID="Container" runat="server" />
		                                </ul>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
	                                <li><asp:Image ID="imgPhoto" runat="server" ImageUrl='<%# Eval("Url") %>' AlternateText='<%# Eval("Title") %>' /></li>
                                </ItemTemplate>
                            </telerik:RadListView>

                        </telerik:RadPageView>
                        <telerik:RadPageView id="pageFloorPlan" runat="server">

                            <telerik:RadListView ID="rlvFloorPlan" runat="server" ItemPlaceholderID="Container" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
                                <LayoutTemplate>
                                    <div class="property-carousel floor-plan">
				                        <ul>
                                            <asp:PlaceHolder ID="Container" runat="server" />
		                                </ul>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
	                                <li><asp:Image ID="imgPhoto" runat="server" ImageUrl='<%# Eval("Url") %>' AlternateText='<%# Eval("Title") %>' /></li>
                                </ItemTemplate>
                            </telerik:RadListView>

                        </telerik:RadPageView>
                        <telerik:RadPageView id="pageMap" runat="server">
                        
                            <div id="gmap"></div>

                            <script language="javascript" type="text/javascript">
                                $(document).ready(function () {
                                    mapIt('<%# Eval("Latitude") %>', '<%# Eval("Longitude") %>');
                                });
                            </script>

                        </telerik:RadPageView>
                    </telerik:RadMultiPage>

                    <div class="column">
					    <h1><%#Eval("Title") %></h1>
					    <address><%#Eval("Address") %> <asp:Literal ID="ltrLocation" runat="server" /></address>
					
					    <asp:Panel ID="pnlPrice" runat="server" CssClass="price">$price$</asp:Panel>
					    <br style="clear:both;" />
					    <dl>
						    <dt><%$Resources:RealEstateResources, ItemNumber %>:</dt> 						<dd><%#Eval("ItemNumber") %></dd>

                            <asp:PlaceHolder ID="phItemType" runat="server" Visible="false">
						        <dt><%$Resources:RealEstateResources, Type %>:</dt> 								<dd><asp:Literal ID="ltrItemType" runat="server" /></dd>
                            </asp:PlaceHolder>

                            <asp:PlaceHolder ID="phHousing" runat="server" Visible="false">
						        <dt><%$Resources:RealEstateResources, Housing %>:</dt>			 				<dd><asp:Literal ID="ltrHousing" runat="server" /></dd>
						    </asp:PlaceHolder>
                        
                            <asp:PlaceHolder ID="phRooms" runat="server" Visible="false">
						        <dt><%$Resources:RealEstateResources, Rooms %>:</dt>			 				<dd><asp:Literal ID="ltrRooms" runat="server" /></dd>
						    </asp:PlaceHolder>

                            <asp:PlaceHolder ID="phFloors" runat="server" Visible="false">
						        <dt><%$Resources:RealEstateResources, Floors %>:</dt>			 				<dd><asp:Literal ID="ltrFloors" runat="server" /></dd>
						    </asp:PlaceHolder>

                            <asp:PlaceHolder ID="phBuilt" runat="server" Visible="false">
						        <dt><%$Resources:RealEstateResources, YearBuilt %>:</dt>			 				<dd><asp:Literal ID="ltrBuilt" runat="server" /></dd>
						    </asp:PlaceHolder>

                            <asp:PlaceHolder ID="phPayment" runat="server" Visible="false">
						        <dt class="alt"><%$Resources:RealEstateResources, Payment %>:</dt>			 				<dd class="alt"><asp:Literal ID="ltrPayment" runat="server" /></dd>
						    </asp:PlaceHolder>

                            <asp:PlaceHolder ID="phMonthlyRate" runat="server" Visible="false">
						        <dt class="alt"><%$Resources:RealEstateResources, MonthlyRate %>:</dt>			 				<dd class="alt"><asp:Literal ID="ltrMonthlyRate" runat="server" /></dd>
						    </asp:PlaceHolder>

                            <asp:PlaceHolder ID="phNet" runat="server" Visible="false">
						        <dt class="alt"><%$Resources:RealEstateResources, Net %>:</dt>			 				<dd class="alt"><asp:Literal ID="ltrNet" runat="server" /></dd>
						    </asp:PlaceHolder>

                            <asp:PlaceHolder ID="phPriceSquareMeter" runat="server" Visible="false">
						        <dt class="alt"><%$Resources:RealEstateResources, PriceSquareMeter %>:</dt>			 				<dd class="alt"><asp:Literal ID="ltrPriceSquareMeter" runat="server" /></dd>
						    </asp:PlaceHolder>

					    </dl>
				    </div>
                </div>

                <telerik:RadListView ID="FeaturesList" runat="server" ItemPlaceholderID="ItemContainer" GroupPlaceHolderID="GroupContainer" GroupItemCount="4" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
                    <LayoutTemplate>
			            <div class="column">
				            <div class="block block-features">
					            <h2><%$Resources:RealEstateResources, Features %></h2>
					            <div class="block-content">
						        
                                    <asp:PlaceHolder ID="GroupContainer" runat="server" />

					            </div>
				            </div>
			            </div>
                    </LayoutTemplate>
                    <GroupTemplate>
                        <ul>
                            <asp:PlaceHolder ID="ItemContainer" runat="server" />
                        </ul>
                    </GroupTemplate>
                    <ItemTemplate>
	                    <li><asp:Literal ID="ltrFeature" runat="server" /></li>
                    </ItemTemplate>
                </telerik:RadListView>

                <asp:PlaceHolder ID="phAgent" runat="server" Visible="false">
			        <div class="column">
				        <div class="block block-contact">
					        <h2><%$Resources:RealEstateResources, ContactInformation %></h2>
					        <div class="block-content">
						        <asp:PlaceHolder ID="phAgentAddress" runat="server" Visible="false"><address><asp:Literal ID="ltrAgentAddress" runat="server" /></address></asp:PlaceHolder>
						        <dl>
							        <dt><%$Resources:RealEstateResources, Name %>:</dt> <dd><asp:Literal ID="ltrAgentName" runat="server" /></dd>

                                    <asp:PlaceHolder ID="phAgentPhoneNumber" runat="server" Visible="false">
						                <dt><%$Resources:RealEstateResources, Telephone %>:</dt>    <dd><asp:Literal ID="ltrAgentPhoneNumber" runat="server" /></dd>
						            </asp:PlaceHolder>

                                    <asp:PlaceHolder ID="phAgentEmail" runat="server" Visible="false">
						                <dt><%$Resources:RealEstateResources, Email %>:</dt>    <dd><asp:HyperLink ID="hlAgentEmail" CssClass="contactAgent" runat="server" /></dd>
						            </asp:PlaceHolder>
						        </dl>
					        </div>
				        </div>
			        </div>
                </asp:PlaceHolder>

                <asp:PlaceHolder ID="phDescription" runat="server" Visible="false">
			        <div class="column">
				        <div class="block block-description">
					        <h2>Description</h2>
					        <div class="block-content">
						        <dd><%#Eval("Description") %></dd>
					        </div>
				        </div>
			        </div>
                </asp:PlaceHolder>	

                <telerik:RadListView ID="RoomsList" runat="server" ItemPlaceholderID="ItemContainer" GroupPlaceHolderID="GroupContainer" GroupItemCount="4" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
                    <LayoutTemplate>
			            <div class="column">
				            <div class="block block-features">
					            <h2><%$Resources:RealEstateResources, Rooms %></h2>
					            <div class="block-content">
						        
                                    <asp:PlaceHolder ID="GroupContainer" runat="server" />

					            </div>
				            </div>
			            </div>
                    </LayoutTemplate>
                    <GroupTemplate>
                        <ul>
                            <asp:PlaceHolder ID="ItemContainer" runat="server" />
                        </ul>
                    </GroupTemplate>
                    <ItemTemplate>
	                    <li><asp:Literal ID="ltrItem" runat="server" /></li>
                    </ItemTemplate>
                </telerik:RadListView>
            </div>            		
        </ItemTemplate>
    </telerik:RadListView>
</div>

<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        // globals
        var speed = 500,
		speedFast = Math.round(speed / 2);

        /*---------------------------------------------------------------------------------- CAROUSELS */


        var propertyCarousel = $('#' + initialTabStripId + ' .property-carousel');
        propertyCarousel.jcarousel({
            scroll: 1,
            duration: speedFast,
            initCallback: function (c) {
                var carousel = c.container;
                carousel.append('<div class="info"><span class="left" /><span class="right" /></div>');

                var info = carousel.find('.info'),
				title = info.find('.left'),
				counter = title.next(),
				prev = carousel.find('.jcarousel-prev'),
				next = prev.next(),
				imgs = carousel.find('img'),
				img = imgs.filter(':first');

                title.text(img[0].alt);
                counter.text('1/' + imgs.length);

                next.click(function (e) {
                    img = img.parents('li').next().find('img');
                    if (img.length) {
                        var num = parseInt(img.parent().attr("jcarouselindex"));
                        updateCarouselBottomText(img[0].alt, num);

                    } else img = imgs.filter(':last');
                });

                prev.click(function (e) {
                    img = img.parents('li').prev().find('img');
                    if (img.length) {
                        var num = parseInt(img.parent().attr("jcarouselindex"));
                        updateCarouselBottomText(img[0].alt, num);

                    } else img = imgs.filter(':first');
                });

                function updateCarouselBottomText(text, num) {
                    title.text(text);
                    counter.text(num + '/' + imgs.length);
                }
            }
        });

    });
</script>

<script language="javascript" type="text/javascript">
	$(document).ready(function () {

		// globals
		var speed = 500,
    		speedFast = Math.round(speed / 2);
		jQuery('.contactAgent').fancybox({
			padding: 30,
			autoDimensions: false,
			width: 780,
			height: 575,
			overlayColor: '#000',
			overlayOpacity: 0.7,
			speedIn: speedFast,
			speedOut: speedFast,
			changeSpeed: speedFast,
			centerOnScroll: true,
			onComplete: function () {
				var div = $('<div style="display:none" />').appendTo(document.body);
				var fb = $('#fancybox-content');
				fb.find('.agent-single').appendTo(div);
				fb.html('');
				fb.append(div.children());
			}
		});


	});


    function mapIt(lat, lng) {
        if (GBrowserIsCompatible()) {

            lat = lat.replace(",", ".");
            lng = lng.replace(",", ".");

            var point = new GLatLng(parseFloat(lat), parseFloat(lng));

            map = new GMap2(document.getElementById("gmap"), { size: new GSize(749, 437) });
            map.setCenter(point, 13);

            var mapTypeControl = new GMapTypeControl();
            var topRight = new GControlPosition(G_ANCHOR_TOP_RIGHT, new GSize(10, 10));
            var bottomRight = new GControlPosition(G_ANCHOR_BOTTOM_RIGHT, new GSize(10, 10));
            map.addControl(mapTypeControl, topRight);
            map.addControl(new GLargeMapControl3D());

            map.addOverlay(new GMarker(point));

            map.panTo(point);
        }
    }

    function tabSelectedHandler(sender, eventArgs) {
        var tab = eventArgs.get_tab();
        var pageViewId = tab.get_pageViewID();

        if (pageViewId == null || pageViewId == "undefined") {
        	return;
        }

        // globals
        var speed = 500,
		speedFast = Math.round(speed / 2);

        /*---------------------------------------------------------------------------------- CAROUSELS */

        var propertyCarousel = $('#' + pageViewId + ' .property-carousel');
        propertyCarousel.jcarousel({
            scroll: 1,
            duration: speedFast,
            initCallback: function (c) {
                var carousel = c.container;
                carousel.append('<div class="info"><span class="left" /><span class="right" /></div>');

                var info = carousel.find('.info'),
				title = info.find('.left'),
				counter = title.next(),
				prev = carousel.find('.jcarousel-prev'),
				next = prev.next(),
				imgs = carousel.find('img'),
				img = imgs.filter(':first');

                title.text(img[0].alt);
                counter.text('1/' + imgs.length);

                next.click(function (e) {
                    img = img.parents('li').next().find('img');
                    if (img.length) {
                        var num = parseInt(img.parent().attr("jcarouselindex"));
                        updateCarouselBottomText(img[0].alt, num);

                    } else img = imgs.filter(':last');
                });

                prev.click(function (e) {
                    img = img.parents('li').prev().find('img');
                    if (img.length) {
                        var num = parseInt(img.parent().attr("jcarouselindex"));
                        updateCarouselBottomText(img[0].alt, num);

                    } else img = imgs.filter(':first');
                });

                function updateCarouselBottomText(text, num) {
                    title.text(text);
                    counter.text(num + '/' + imgs.length);
                }
            }
        });
    }
</script>