$(() => {
    $(".notes-toggle").on('click', function() {
        $('td:nth-child(5),th:nth-child(5)').toggle();
    });
});