/*function videoBlocks(){
	if(screen.width > 767){
			$(".regular3").slick({
				dots: true,
				infinite: true,
				slidesToShow: 3,
				slidesToScroll: 3
		   });
		  } else if(screen.width > 480)
		  {
			  $(".regular3").slick({
					dots: true,
					infinite: true,
					slidesToShow: 2,
					slidesToScroll: 2
			   });
		  } else 
		  {
			  $(".regular3").slick({
					dots: true,
					infinite: true,
					slidesToShow: 1,
					slidesToScroll: 1
			   });
		  }
}*/

$(document).on('ready', function() {
      
	  
	   /*------*/
	   $('.regular').slick({
				  dots: true,
				  infinite: false,
				  speed: 300,
				  slidesToShow: 4,
				  slidesToScroll: 4,
				  responsive: [
					{
					  breakpoint: 1024,
					  settings: {
						slidesToShow: 4,
						slidesToScroll: 4,
						infinite: true,
						dots: true
					  }
					},
					{
					  breakpoint: 600,
					  settings: {
						slidesToShow: 4,
						slidesToScroll: 4
					  }
					},
					{
					  breakpoint: 480,
					  settings: {
						slidesToShow: 4,
						slidesToScroll:4
					  }
					}
					// You can unslick at a given breakpoint now by adding:
					// settings: "unslick"
					// instead of a settings object
				  ]
				});
		  /*============*/
		  
		  /*------*/
		 $('.regular3').slick({
				  dots: true,
				  infinite: false,
				  speed: 300,
				  slidesToShow: 3,
				  slidesToScroll: 3,
				  responsive: [
					{
					  breakpoint: 1024,
					  settings: {
						slidesToShow: 2,
						slidesToScroll: 2,
						infinite: true,
						dots: true
					  }
					},
					{
					  breakpoint: 600,
					  settings: {
						slidesToShow: 2,
						slidesToScroll: 2
					  }
					},
					{
					  breakpoint: 480,
					  settings: {
						slidesToShow: 1,
						slidesToScroll: 1
					  }
					}
					// You can unslick at a given breakpoint now by adding:
					// settings: "unslick"
					// instead of a settings object
				  ]
				});
		  
		  /*============*/
		  
		
					  
	  
	  
	  
      $(".center").slick({
        dots: true,
        infinite: true,
        centerMode: true,
        slidesToShow: 5,
        slidesToScroll: 5
      });
      $(".variable").slick({
        dots: true,
        infinite: true,
        variableWidth: true
      });
    });// JavaScript Document