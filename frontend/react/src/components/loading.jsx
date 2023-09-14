import classes from "./loading.module.css";

function Loading({ isVisible }) {
  return (
    <div className={`${isVisible ? classes.loading : classes.hidden}`}>
      <p className={classes.text}>Ładowanie...</p>
    </div>
  );
}

export default Loading;
