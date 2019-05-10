$(() => {
    const instructorsApiUrl = `${window.location.origin}/api/instructors`;

    $.get(instructorsApiUrl)
        .then((response) => {
            const prettifiedJson = JSON.stringify(response, null, '\t');
            $('#instructordata').text(prettifiedJson);
        });
});