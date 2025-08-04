/*
    Soak Testing is used to validate reliability of the system over a long time

    Run a soak test to:
    - Verify that your system doesn't suffer from bugs or memoer leaks, which result in a crash or restart after crash
    - Verify that expected application restarts don't lose requests
    - Find bugs related to race-conditions that appear sporadically
    - Make sure your logs don't exhaust the allotted disk storage
    - Make sure your database doesn't exhaust the allotted storage space and stops
    - Make sure the external services you depend on do't stop working after a certain amount if requests are executed

    How to run a soak test:
    - Determe the maximun amount of users your system can handle
    - Get the 75-80% fo that value
    - Set VUs to that value
    - Run the test in w stages. Rump to the VUs, staty there for 4-12 hours, rump down to 0
*/