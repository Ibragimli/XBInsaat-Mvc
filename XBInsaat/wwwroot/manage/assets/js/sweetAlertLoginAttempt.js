﻿$(function () {
    $(document).on("click", ".attempt-count-btn", function (e) {
        e.preventDefault();
        let url = $(this).attr("href");
        Swal.fire({
            title: 'Limiti yeniləmək istəyirsinizmi?',
            icon: 'question',
            showCancelButton: true,
            confirmButtonColor: '#32CD32',
            iconColor: "#32CD32",
            cancelButtonColor: '#d33',
            confirmButtonText: 'Bəli',
            cancelButtonText: 'Xeyr',
            width: "26em",
        }).then((result) => {
            if (result.isConfirmed) {
                fetch(url)
                    .then(response => {
                        if (response.ok) {
                            window.location.reload(true)
                        }
                        else {
                            alert("xeta bas verdi")
                            console.log(url)
                            console.log(response.statusText)
                        }
                    })
            }
            else {
                console.log("cancel")
            }
        })
    })
})