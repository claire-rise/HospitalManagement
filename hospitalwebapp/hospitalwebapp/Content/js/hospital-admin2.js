// Script for image upload.........................................................
    $(document).on("click", ".browse", function(){
         

      var file = $(this).parents().find("file");
      file.trigger("click");
      });

    $('input[type = "file"]').change(function(e) {

      var fileName = e.target.files[0].name;
      // $("file").val(fileName);  //Take note of this line


      var reader = new FileReader();
      reader.onload = function(e){
          // get loaded data and render thumbnail.
          document.getElementById("preview").src = e.target.result;
          document.getElementById("test").innerHTML = fileName;
           
        };
        // read the image file as a data URL.
        reader.readAsDataURL(this.files[0]);
        
      }); //End of image script

    
//Script for modal form reset on close
    $(document).ready(function() {

        $('#userRegisterModal').on('hidden.bs.modal',  function() {

          $('#preview').trigger("reset");
          document.getElementById("preview").src = null;
          $('#test').text("");
          $('#userRegisterModal').trigger("reset");

          
          });
      });  //End of modal reset script.....................................................

     const $dropdown = $(".dropdown");
const $dropdownToggle = $(".dropdown-toggle");
const $dropdownMenu = $(".dropdown-menu");
const showClass = "show";
 
$(window).on("load resize", function() {
  if (this.matchMedia("(min-width: 768px)").matches) {
    $dropdown.hover(
      function() {
        const $this = $(this);
        $this.addClass(showClass);
        $this.find($dropdownToggle).attr("aria-expanded", "true");
        $this.find($dropdownMenu).addClass(showClass);
      },
      function() {
        const $this = $(this);
        $this.removeClass(showClass);
        $this.find($dropdownToggle).attr("aria-expanded", "false");
        $this.find($dropdownMenu).removeClass(showClass);
      }
    );
  } else {
    $dropdown.off("mouseenter mouseleave");
  }
});
// ........................................................................