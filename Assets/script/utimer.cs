/*
 * microtimer, as in it won't accept a callback function.
 * start_time should be Time.time.
 * duration is measured in seconds.
 */
public struct utimer {
	public float duration;
	public float start_time;

	/*
	 * Expects to recieve Time.time.
	 */
	public bool done(float time) { return time - start_time >= duration; }
}
