$(".toggle-password").click(function () {

	$(this).toggleClass("fa-eye fa-eye-slash");
	var input = $($(this).attr("toggle"));
	if (input.attr("type") == "password") {
		input.attr("type", "text");
	} else {
		input.attr("type", "password");
	}
});

/* Dropdown Menu for Admin-Members Page */
function myAdminMembersDropdown(id) {
  document.getElementById("myDropdown_" + id).classList.toggle("show");
}

/* Dropdown Menu for Spam-Report Page */
function mySpamReportDropdown(id) {
  document.getElementById("myDropdown_" + id).classList.toggle("show");
}

/* Dropdown Menu for Published-Notes Page */
function myPublishedNotesDropdown(id) {
  document.getElementById("myDropdown_" + id).classList.toggle("show");
}

/* Dropdown Menu for Rejected-Notes Page */
function myRejectedNotesDropdown(id) {
  document.getElementById("myDropdown_" + id).classList.toggle("show");
}

/* Dropdown Menu for Member-Details Page */
function myMemberDetailsDropdown(id) {
  document.getElementById("myDropdown_" + id).classList.toggle("show");
}

/* Dropdown Menu for Download-Notes Page */
function myDownloadedNotesDropdown(id) {
  document.getElementById("myDropdown_" + id).classList.toggle("show");
}

/* Dropdown Menu for Dashboard Page */
function myDashboardDropdown(id) {
  document.getElementById("myDropdown_" + id).classList.toggle("show");
}

// Close the dropdown if the user clicks outside of it
window.onclick = function(event) {
  if (!event.target.matches('.dropbtn')) {
    var dropdowns = document.getElementsByClassName("dropdown-content");
    var i;
    for (i = 0; i < dropdowns.length; i++) {
      var openDropdown = dropdowns[i];
      if (openDropdown.classList.contains('show')) {
        openDropdown.classList.remove('show');
      }
    }
  }
}

//Validatons for File Upload
function picturevalidation() {
	var fileInput = document.getElementById('upload-picture');
	var filepath = fileInput.value;
	var allowedextensions = /(\.jpg|\.jpeg|\.png|\.gif)$/i;

	if (!allowedextensions.exec(filepath)) {
		fileInput.value = '';
		document.getElementById('uploadpicture-error').innerHTML = "Image should be .jpg , .jpeg or .png file only";
		return false;
	}
	else {
		document.getElementById('uploadpicture-error').innerHTML = "";
	}
}