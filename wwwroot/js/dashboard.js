<script>
    var deleteUrl = "";
    document.querySelectorAll('[data-bs-toggle="modal"]').forEach(button => {
        button.addEventListener('click', function () {
            deleteUrl = "/KYC/Delete/" + this.getAttribute("data-id");
        });
    });

    document.getElementById('confirmDelete').addEventListener('click', function () {
        window.location.href = deleteUrl;
    });
</script>