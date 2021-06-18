$(document).ready(function () {

    $('.DeleteButton').click(function () {
        var self = $(this);
        var id = self.attr('id');

        $.ajax({
            url: '/ToDoList/DeleteToDoItem',
            data: {
                id: id
            },
            type: 'POST',
            success: function (result) {
                $('#tableDiv').html(result);
            }
        });

    });

});