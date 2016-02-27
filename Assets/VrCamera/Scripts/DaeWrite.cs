using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public static class DaeWrite {

	static string xmlBeforeString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<COLLADA xmlns=\"http://www.collada.org/2008/03/COLLADASchema\" version=\"1.5.0\">\n\t<asset>\n\t\t<contributor>\n\t\t\t<authoring_tool>CINEMA4D 16.050 COLLADA Exporter</authoring_tool>\n\t\t</contributor>\n\t\t<created>2015-12-12T23:09:24Z</created>\n\t\t<modified>2015-12-12T23:09:24Z</modified>\n\t\t<unit meter=\"0.01\" name=\"centimeter\"/>\n\t\t<up_axis>Y_UP</up_axis>\n\t</asset>\n\t<library_geometries>\n\t\t<geometry id=\"ID102\">\n\t\t\t<mesh>\n\t\t\t\t<source id=\"ID103\">\n\t\t\t\t\t<float_array id=\"ID104\" count=\"24\" digits=\"2490374\">-0.1 -0.1 0.1 -0.1 0.1 0.1 0.1 -0.1 0.1 0.1 0.1 0.1 0.1 -0.1 -0.1 0.1 0.1 -0.1 -0.1 -0.1 -0.1 -0.1 0.1 -0.1</float_array>\n\t\t\t\t\t<technique_common>\n\t\t\t\t\t\t<accessor count=\"8\" source=\"#ID104\" stride=\"3\">\n\t\t\t\t\t\t\t<param name=\"X\" type=\"float\"/>\n\t\t\t\t\t\t\t<param name=\"Y\" type=\"float\"/>\n\t\t\t\t\t\t\t<param name=\"Z\" type=\"float\"/>\n\t\t\t\t\t\t</accessor>\n\t\t\t\t\t</technique_common>\n\t\t\t\t</source>\n\t\t\t\t<source id=\"ID105\">\n\t\t\t\t\t<float_array id=\"ID106\" count=\"18\" digits=\"2490374\">0 0 1 1 0 -0 0 0 -1 -1 0 -0 0 1 -0 0 -1 -0</float_array>\n\t\t\t\t\t<technique_common>\n\t\t\t\t\t\t<accessor count=\"6\" source=\"#ID106\" stride=\"3\">\n\t\t\t\t\t\t\t<param name=\"X\" type=\"float\"/>\n\t\t\t\t\t\t\t<param name=\"Y\" type=\"float\"/>\n\t\t\t\t\t\t\t<param name=\"Z\" type=\"float\"/>\n\t\t\t\t\t\t</accessor>\n\t\t\t\t\t</technique_common>\n\t\t\t\t</source>\n\t\t\t\t<source id=\"ID107\">\n\t\t\t\t\t<float_array id=\"ID108\" count=\"2\" digits=\"2490374\">0 1</float_array>\n\t\t\t\t\t<technique_common>\n\t\t\t\t\t\t<accessor count=\"1\" source=\"#ID108\" stride=\"2\">\n\t\t\t\t\t\t\t<param name=\"S\" type=\"float\"/>\n\t\t\t\t\t\t\t<param name=\"T\" type=\"float\"/>\n\t\t\t\t\t\t</accessor>\n\t\t\t\t\t</technique_common>\n\t\t\t\t</source>\n\t\t\t\t<vertices id=\"ID109\">\n\t\t\t\t\t<input semantic=\"POSITION\" source=\"#ID103\"/>\n\t\t\t\t</vertices>\n\t\t\t\t<polylist count=\"6\">\n\t\t\t\t\t<input offset=\"0\" semantic=\"VERTEX\" source=\"#ID109\" set=\"0\"/>\n\t\t\t\t\t<input offset=\"1\" semantic=\"NORMAL\" source=\"#ID105\" set=\"0\"/>\n\t\t\t\t\t<input offset=\"2\" semantic=\"TEXCOORD\" source=\"#ID107\" set=\"0\"/>\n\t\t\t\t\t<vcount>4 4 4 4 4 4</vcount>\n\t\t\t\t\t<p>2 0 0 3 0 0 1 0 0 0 0 0 4 1 0 5 1 0 3 1 0 2 1 0 6 2 0 7 2 0 5 2 0 4 2 0 0 3 0 1 3 0 7 3 0 6 3 0 3 4 0 5 4 0 7 4 0 1 4 0 4 5 0 2 5 0 0 5 0 6 5 0</p>\n\t\t\t\t</polylist>\n\t\t\t</mesh>\n\t\t</geometry>\n\t</library_geometries>";

	static string xmlAfterString = "\t<library_visual_scenes>\n\t\t<visual_scene id=\"ID1\">\n\t\t\t<node id=\"ID2\" name=\"Cube\">\n\t\t\t\t<translate sid=\"translate\">-258.343 162.098 -537.524</translate>\n\t\t\t\t<rotate sid=\"rotateY\">0 1 0 69.569</rotate>\n\t\t\t\t<rotate sid=\"rotateX\">1 0 0 40.7981</rotate>\n\t\t\t\t<rotate sid=\"rotateZ\">0 0 1 -62.0643</rotate>\n\t\t\t\t<scale sid=\"scale\">1 1 1</scale>\n\t\t\t\t<instance_geometry url=\"#ID102\"/>\n\t\t\t</node>\n\t\t\t<node id=\"ID110\" name=\"myOctaneSettings\">\n\t\t\t\t<translate sid=\"translate\">0 0 -0</translate>\n\t\t\t\t<rotate sid=\"rotateY\">0 1 0 -0</rotate>\n\t\t\t\t<rotate sid=\"rotateX\">1 0 0 0</rotate>\n\t\t\t\t<rotate sid=\"rotateZ\">0 0 1 -0</rotate>\n\t\t\t\t<scale sid=\"scale\">1 1 1</scale>\n\t\t\t</node>\n\t\t</visual_scene>\n\t</library_visual_scenes>\n\t<scene>\n\t\t<instance_visual_scene url=\"#ID1\"/>\n\t</scene>\n</COLLADA>";

	static public void CreateDae(float[] times, Vector3[] positions, Vector3[] rotations) {
		string fileName = "Assets/recordings/created" + System.DateTime.UtcNow.ToString("HH_mm_ss_fff__dd-MMMM-yyyy") + ".dae";
		var file = File.CreateText(fileName);
		file.WriteLine (xmlBeforeString);
		file.WriteLine (XmlMiddleString(times, positions, rotations));
		file.WriteLine (xmlAfterString);
		file.Close();
	}
	
	static string LinearForFrames(int frames) {
        StringBuilder sb = new StringBuilder();
		for (int i = 0; i< frames; i++){
            sb.Append("LINEAR");		 
			if (i != frames-1) {
				sb.Append(" ");
			}
		}
		return sb.ToString();
	}

	static float[] Pluck(Vector3[] array, string fieldName) {

		float[] arrayOfValues = new float[array.Length];

		for (int i = 0; i < array.Length; i++) {
			arrayOfValues[i] = (float) array[i].GetType().GetField(fieldName).GetValue(array[i]);
		}

		return arrayOfValues;
	}

	static string ArrayToStringSeperatedValues(float[] array) {
        StringBuilder sb = new StringBuilder();
		for (int i = 0; i < array.Length; i++){
            sb.Append(array[i].ToString()); 
			if (i != array.Length-1) {
				sb.Append(" ");
			}
		}
		return sb.ToString();
	}

	static string ZipperArraysToStringSeperatedValues(float[] array1, float[] array2) {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < array1.Length; i++){
			sb.Append(array1[i].ToString() + " "); 
			sb.Append(array2[i].ToString());
			if (i != array1.Length-1) {
				sb.Append(" ");
			}
		}
		return sb.ToString();
	}
	
	static string GetAnimationCurveXMLTemplate (int startingId, string curveName, float[] curveValues, float[] times) {
		int keyframes = curveValues.Length;
		int doublekeyframes = keyframes * 2;
		string[] ids = new string[11];
		for (int i = 0; i < 11; i++) {
			ids[i] = (startingId+i).ToString();
		} 

		return "<animation>"
			+ "<source id=\"ID" + ids[0] + "\">"
			+ "<float_array id=\"ID" + ids[1] + "\" count=\""+ keyframes +"\" digits=\"2490374\">" + ArrayToStringSeperatedValues(times) + "</float_array>"
					+ "<technique_common>"
						+ "<accessor count=\""+ keyframes +"\" source=\"#ID" + ids[1] + "\">"
							+ "<param name=\"TIME\" type=\"float\"/>"
						+ "</accessor>"
					+ "</technique_common>"
				+ "</source>"
				+ "<source id=\"ID" + ids[2]  +"\">"
					+ "<Name_array id=\"ID" + ids[3] + "\" count=\""+ keyframes +"\">" + LinearForFrames(keyframes) + "</Name_array>"
					+ "<technique_common>"
						+ "<accessor count=\""+ keyframes +"\" source=\"#ID" + ids[3] + "\">"
							+ "<param name=\"INTERPOLATION\" type=\"Name\"/>"
						+ "</accessor>"
					+ "</technique_common>"
				+ "</source>"
				+ "<source id=\"ID" + ids[4] + "\">"
					+ "<float_array id=\"ID" + ids[5] + " \" count=\""+ doublekeyframes +"\" digits=\"2490374\"> " + ZipperArraysToStringSeperatedValues(times, curveValues) + "</float_array>"
					+ "<technique_common>"
						+ "<accessor count=\""+ keyframes +"\" source=\"#ID" + ids[5] + "\" stride=\"2\">"
							+ "<param name=\"X\" type=\"float\"/>"
							+ "<param name=\"Y\" type=\"float\"/>"
						+ "</accessor>"
					+ "</technique_common>"
				+ "</source>"
			+ "<source id=\"ID" + ids[6] +"\">"
			+ "<float_array id=\"ID" + ids[7] + "\" count=\""+ doublekeyframes +"\" digits=\"2490374\">" + ZipperArraysToStringSeperatedValues(times, curveValues) + "</float_array>"
					+ "<technique_common>"
						+ "<accessor count=\""+ keyframes +"\" source=\"#ID" + ids[7] + "\" stride=\"2\">"
							+ "<param name=\"X\" type=\"float\"/>"
							+ "<param name=\"Y\" type=\"float\"/>"
						+ "</accessor>"
					+ "</technique_common>"
				+ "</source>"
				+ "<source id=\"ID" + ids[8] +"\">"
					+ "<float_array id=\"ID" + ids[9] + "\" count=\""+ keyframes +"\" digits=\"2490374\">" + ArrayToStringSeperatedValues(curveValues) + "</float_array>"
					+ "<technique_common>"
						+ "<accessor count=\""+ keyframes +"\" source=\"#ID" + ids[9] +"\">"
							+ "<param name=\"X\" type=\"float\"/>"
						+ "</accessor>"
					+ "</technique_common>"
				+ "</source>"
			+ "<sampler id=\"ID" + ids[10] +"\" pre_behavior=\"CONSTANT\" post_behavior=\"CONSTANT\">"
			+ "<input semantic=\"INPUT\" source=\"#ID"+ ids[0] +"\"/>"
			+ "<input semantic=\"INTERPOLATION\" source=\"#ID" + ids[2] + "\"/>"
					+ "<input semantic=\"IN_TANGENT\" source=\"#ID" + ids[4] + "\"/>"
					+ "<input semantic=\"OUT_TANGENT\" source=\"#ID" + ids[6] + "\"/>"
					+ "<input semantic=\"OUTPUT\" source=\"#ID" + ids[8] +"\"/>"
				+ "</sampler>"
				+ "<channel source=\"#ID" + ids[10] + "\" target=\"ID2/" + curveName + "\"/>"
			+ "</animation>";
	}

	static string XmlMiddleString(float[] times, Vector3[] positions, Vector3[] rotations) {
		float[] xPositions = Pluck(positions, "x");
		float[] yPositions = Pluck(positions, "y");
		float[] zPositions = Pluck(positions, "z");
		float[] xRotations = Pluck(rotations, "x");
		float[] yRotations = Pluck(rotations, "y");
		float[] zRotations = Pluck(rotations, "z");

		string translateX = GetAnimationCurveXMLTemplate(3, "translate.X", xPositions, times);
		string translateY = GetAnimationCurveXMLTemplate(14, "translate.Y", yPositions, times);
		string translateZ = GetAnimationCurveXMLTemplate(25, "translate.Z", zPositions, times);
		string rotateY = GetAnimationCurveXMLTemplate(36, "rotateY.ANGLE", yRotations, times);
		string rotateX = GetAnimationCurveXMLTemplate(47, "rotateX.ANGLE", xRotations, times);
		string rotateZ = GetAnimationCurveXMLTemplate(58, "rotateZ.ANGLE", zRotations, times);

		return "<library_animations><animation>" + translateX + translateY + translateZ + rotateY + rotateX + rotateZ + "</animation></library_animations>";
	}
}
