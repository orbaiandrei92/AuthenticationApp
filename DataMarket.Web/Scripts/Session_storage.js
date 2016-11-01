function clearSession() {
    if (sessionStorage != null) {
        sessionStorage.clear();
    }
    console.log("SS deleted");
}