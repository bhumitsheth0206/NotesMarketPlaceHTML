$(function () {

    showHideNav()
  
    $(window).scroll(function(){
  
        showHideNav()
  
    });
  
    function showHideNav() {
  
        if($(window).scrollTop() > 120){
  
            $("nav").removeClass("navbar-dark");
            $("nav").removeClass("bg-transparent");
            $("nav").addClass("navbar-light");
            $("nav").addClass("bg-light");
            $("nav").addClass("general-navbar");

            $(".homebtn").removeClass("btn-home-login");
            $(".homebtn").addClass("btn-nav-login");

            $(".navbar-brand img").attr("src", "/Content/User/User-Profile/logo.png");
  
        }
        else {
  
            $("nav").removeClass("navbar-light");
            $("nav").removeClass("bg-light");
            $("nav").removeClass("general-navbar");
            $("nav").addClass("navbar-dark");
            $("nav").addClass("bg-transparent");

            $(".homebtn").removeClass("btn-nav-login");
            $(".homebtn").addClass("btn-home-login");

            $(".navbar-brand img").attr("src", "/Content/User/pre-login/top-logo.png");
  
        }
  
    }
  
  });