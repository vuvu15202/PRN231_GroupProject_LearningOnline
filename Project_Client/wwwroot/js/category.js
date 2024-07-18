$(document).ready(function() {
    $.ajax({
        url: 'https://localhost:5000/api/Categories',
        method: 'GET',
        dataType: 'json',
        success: function(data) {
            var select = $('#courseSelect');
            //console.log('Select element:', select.document);
            console.log('HTML của select:', select.prop('outerHTML'));
            $.each(data, function(index, category) {
                console.log('Adding category:', category.name);
                select.append($('<option></option>')
                    .attr('value', category.categoryId)
                    .text(category.name));
            });
            console.log('HTML của select:', select.prop('outerHTML'));
        },
        error: function(xhr, status, error) {
            console.error('Error: abc', error);
        }
    });
});