$(".details").on("click", function () {
  const modalTitle = $(this).parent().find("h5").first().text();
  $(".modal-title").text(modalTitle);
});