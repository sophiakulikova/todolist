$(document).ready(function () {

    $('.ChangedName').change(function () {
        var self = $(this);
        var id = self.attr('id');
        var value = self.val();

        $.ajax({
            url: '/ToDoList/ChangeName',
            data: {
                id: id,
                value: value
            },
            type: 'POST',
            success: function (result) {
                $('#tableDiv').html(result);
            }
        });

    });

});