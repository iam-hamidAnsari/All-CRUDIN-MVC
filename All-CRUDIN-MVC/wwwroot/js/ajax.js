$(document).ready(function () {
    getemp();
    

});

$("#addemp").click(function () {
    var fdata = new FormData($("#form"));

    $.ajax({
        url: '/Ajax/AddEmp',
        type: 'Post',
        data : fdata,
        success: function () {
            alert('Emp Added Succesfully');
            getemp();
        },
        error: function () {
            alert('Something went wrong');
        }
    });
});

$("#update").click(function (e) {
    e.preventDefault();
    var fdata = new FormData($("#form")[0]);
    console.log(fdata);
    $.ajax({
        url: '/Ajax/Edit',
        type: 'POST',
        data: fdata,
        processData: false,
        contentType: false,
        success: function (response) {
            console.log('Emp Updated successfully...');
            window.location.href = '/Ajax/Index';
        },
        error: function () {
            alert('Something went wrong');
        }
    });
});


function getemp(id)
{
    $.ajax({
        url: '/Ajax/GetAllEmp',
        type: 'Get',
       /* contentType: '',*/
        dataType: 'json',
        success: function (result)
        {
            var row = '';
            result.forEach(item => {
                row += `
                    <tr>
                    <td>${item.id}</td>
                     <td>${item.ename}</td>
                      <td>${item.salary}</td>
                       <td>
                       <a class="btn btn-sm btn-success" href="${item.img}">View</a>
                       <a class="btn btn-sm btn-primary" href="${item.img}" download="${item.img}">Download</a>
                      </td>
                       <td>
                            <a class="btn btn-sm btn-warning" href="/Ajax/EditEmp?id=${item.id}"  >Edit</a>
                            <a class="btn btn-sm btn-danger" href="/Ajax/delete?id=${item.id}">Delete</a>
                       </td>
                      </tr>
                `;
                $('#data').html(row);
            });
            
        },
        error: function ()
        {
            alert('something went wrong');
        }
    });
}