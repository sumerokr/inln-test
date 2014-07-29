$('#get-date').datepicker({
    minDate: -7,
    maxDate: -1,
    dateFormat: "dd-mm-yy",
    firstDay: 1,
    onSelect: function () { $('#get-xml').show(); }
});

var getXml = function (date) {
    console.log(date);
    $.ajax({
        type: 'GET',
        url: '/Home/GetXML?date=' + date
    }).done(function () {
        location = "/Home/Index?date=" + date;
    });
};

var getComment = function (id) {
    $.ajax({
        type: 'GET',
        url: '/Home/GetComment/' + id,
        dataType: 'html'
    }).done(function (html) {
        $('.modal-comments').find('tbody').prepend(html);
    });
};

var getComments = function (id) {
    $.ajax({
        type: 'GET',
        url: '/Home/GetComments/' + id,
        dataType: 'html'
    }).done(function (html) {
        $('.modal-comments').find('tbody').append(html);
    });
};

var postForm = function (id) {
    if ($.trim($('#Text').val()).length != 0) {
        $.ajax({
            type: 'POST',
            data: $('#id-form').serialize(),
            url: '/Home/CreateComment/' + id,
            dataType: 'html'
        }).done(function () {
            $('#Text').val('');
            $('.modal-comments').find('.alert').remove();
            getComment(id);
        });
    }
};

var getForm = function (id) {
    $.ajax({
        type: 'GET',
        url: '/Home/CreateComment/' + id,
        dataType: 'html'
    }).done(function (html) {
        $('.modal-form').append(html);
        getComments(id);
        $('#save-comment').on('click', function () {
            postForm(id);
        });
        $('#Text').keydown(function (event) {
            if (event.which == 13) {
                postForm(id);
                event.preventDefault();
            }
        });
    });
};

$('#get-xml').on('click', function () {
    $(this).button('loading');
    var date = $('#get-date').val();
    getXml(date);
    console.log(date);
});

$('.get-comments').on('click', function () {
    var id = $(this).attr('data-id');
    $('.modal-form').empty();
    $('.modal-comments').find('tbody').empty();
    getForm(id);
});
