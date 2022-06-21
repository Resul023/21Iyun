$(document).ready(function () {

    $(".get-modal").click(function (e) {
        e.preventDefault();
        let url = $(this).attr("href");
        
        fetch(url)
            .then(response => {
                
                if (!response.ok) {
                    alert("There are problem in process")
                    return;
                }
                
                return response.text()
                
            })
            
            .then(data => {
                
                if (data) {
                    $("#bookDetailModal .model-content").html(data);
                    $("#bookDetailModal").modal("show");
                    
                }
            })
    })
})
