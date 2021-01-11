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

            $("ul button").removeClass("btn-home-login");
            $("ul button").addClass("btn-nav-login");

            $(".navbar-brand img").attr("src", "img/User-Profile/logo.png");
  
        }
        else {
  
            $("nav").removeClass("navbar-light");
            $("nav").removeClass("bg-light");
            $("nav").removeClass("general-navbar");
            $("nav").addClass("navbar-dark");
            $("nav").addClass("bg-transparent");

            $("ul button").removeClass("btn-nav-login");
            $("ul button").addClass("btn-home-login");

            $(".navbar-brand img").attr("src", "img/pre-login/top-logo.png");
  
        }
  
    }
  
  });