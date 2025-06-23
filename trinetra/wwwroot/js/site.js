// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function(){
  $('#filterForm select').change(function(){
    // Example: reload dashboard with query string
    var qs = $('#filterForm').serialize();
    location.search = qs;
  });
});
