// index.js
//The code below will only run when the browser DOM has fully loaded.
//The "document.ready" basically tells the program to wait till the DOM is fully loaded before it can implement any of the above.
$(document).ready(function () {
  var x = 0;
  var s = "";

  console.log("Hello Pluralsight!");



//The "$" sign is just a shortform of saying jquery
  var theForm = $("#theForm");
  theForm.hide();

//With the "$" and the id of an object in the DOM one can target items by class or id without writing lengthy code
    var button = $("#buyButton");

//The ".on" is basically an OnClick event listener
  button.on("click", function () {
    console.log("Buying Item...");
  });

  var productInfo = $(".products-props li");
  productInfo.on("click", function () {
    console.log("You clicked on " + $(this).text());
  });

  var $loginToggle = $("#loginToggle");
  var $popupForm = $(".popup-form");

  $loginToggle.on("click", function () {
    $popupForm.slideToggle(1000);
  });
});