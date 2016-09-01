$('#navSelect').on('change', function (e) {
    $('#navTab li a').eq($(this).val()).tab('show'); 
});


(function($){
        $(window).on("load",function(){
            $(".contentScroll").mCustomScrollbar();
        });
    })(jQuery);


// Inbox Height Function
$(document).ready(function () {
    function setHeight() {
        windowHeight = $(window).height();
        tabnavHeight = $(".tabDasboard").height();
        navigationHeight = $(".navigation").height();
        footerHeight = $("footer").height();
        TitleHeight = $(".inboxContent .detailMail .title").innerHeight();
        actionBtnHeight = $(".inboxContent .detailMail .title .actionBtn").innerHeight();
        inboxHeight = $(window).height() - tabnavHeight - navigationHeight - footerHeight;

        $('.inboxContent').css('height', inboxHeight);

        $('.inboxContent .Listmail .contentScroll').css('height', inboxHeight - 46);

        $('.inboxContent .detailMail .contentScroll').css('height', inboxHeight - TitleHeight - 59 - actionBtnHeight);

    };
    setHeight();

    $(window).resize(function () {
        setHeight();
    });

});

$.fn.responsiveTabs = function() {
  this.addClass('responsive-tabs');
  this.on('click', 'li.active > a, span.glyphicon', function() {
    this.toggleClass('open');
  }.bind(this));

  this.on('click', 'li:not(.active) > a', function() {
    this.removeClass('open');
  }.bind(this));
};

$('.nav.nav-tabs').responsiveTabs();



jQuery(document).ready(function(){
	
jQuery("#txtEditor").Editor({
	'undo':false,
	'redo':false,
	'splchars':false,
	'unlink':false,
	'insert_img':false,
	'hr_line':false,
	'block_quote':false,
	'source':false,
	'strikeout':false,
	'indent':false,
	'outdent':false,
	'fonts':false,
	'print':false,
	'rm_format':false,
	'status_bar':false,
	'font_size':false,
	'color':false,
	'insert_table':false,
	'select_all':false,
	'togglescreen': false
}
);	

jQuery("._submitWhoweAre").Editor({
    'ol': false,
    'ul': false,
    'undo': false,
    'redo': false,
    'splchars': false,
    'unlink': false,
    'insert_img': false,
    'hr_line': false,
    'block_quote': false,
    'source': false,
    'strikeout': false,
    'indent': false,
    'outdent': false,
    'fonts': false,
    'print': false,
    'rm_format': false,
    'status_bar': false,
    'font_size': false,
    'color': false,
    'insert_table': false,
    'select_all': false,
    'togglescreen': false
}
);

jQuery("._submitWhyWeDo").Editor({
    'ol': false,
    'ul': false,
    'undo': false,
    'redo': false,
    'splchars': false,
    'unlink': false,
    'insert_img': false,
    'hr_line': false,
    'block_quote': false,
    'source': false,
    'strikeout': false,
    'indent': false,
    'outdent': false,
    'fonts': false,
    'print': false,
    'rm_format': false,
    'status_bar': false,
    'font_size': false,
    'color': false,
    'insert_table': false,
    'select_all': false,
    'togglescreen': false
}
);

jQuery("._submitWhatWeDo").Editor({
    'ol': false,
    'ul': false,
    'undo': false,
    'redo': false,
    'splchars': false,
    'unlink': false,
    'insert_img': false,
    'hr_line': false,
    'block_quote': false,
    'source': false,
    'strikeout': false,
    'indent': false,
    'outdent': false,
    'fonts': false,
    'print': false,
    'rm_format': false,
    'status_bar': false,
    'font_size': false,
    'color': false,
    'insert_table': false,
    'select_all': false,
    'togglescreen': false
}
);



jQuery("a[rel^='prettyPhoto']").prettyPhoto();
				
	
 jQuery('.owl-carousel').owlCarousel({
    nav:true,
    responsive:{
        0:{
            items:1
        },
        600:{
            items:3
        }
    }
});

jQuery('.service-gallery').owlCarousel({
    nav:false,
    responsive:{
        0:{
            items:1
        },
        480:{
            items:3
        },
        1500:{
            items:4
        }
    }
});

		
});

var countdiv = $(".serviceItem").length;
if (countdiv < 4) {
    $('.service-gallery .owl-controls').addClass('hide');
}


$('.thumbnail').click(function(){
  	$('.modal-body').empty();
  	var title = $(this).parent('a').attr("title");
  	$('.modal-title').html(title);
  	$($(this).parents('div').html()).appendTo('.modal-body');
  	$('#myModal').modal({show:true});
});


// Header Search Open 
$(".navbar .search a.searchIcon").click(function () {
    $('.searchForm').fadeIn(1000, function() {
       $('.searchForm').addClass('open');
    });
	
	$('.navbar-header .search').addClass('active');
});
$(".navbar .search a.close").click(function () {
    $('.searchForm').fadeOut(1000, function() {
       $('.searchForm').removeClass('open');
    });
	$('.navbar-header .search').removeClass('active');
});


$('.inboxContent .closeIcon').click(function() {
	$(".Listmail").animate({width: '0px'}, 500);

	$(".detailMail").animate({
    'marginLeft': '0px'
}, 500);

	$(this).addClass('hide');
	$('.inboxContent .openIcon').removeClass('hide');
});

$('.inboxContent .openIcon').click(function() {
	$(".Listmail").animate({width: '277px'}, 500);
	$(".detailMail").animate({ 'marginLeft': '277px'}, 500);

	$(this).addClass('hide');;
	$('.inboxContent .closeIcon').removeClass('hide');
});

